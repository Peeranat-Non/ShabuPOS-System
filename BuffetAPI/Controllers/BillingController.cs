using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BuffetAPI.Data;   // ⚠️ เช็คชื่อ Namespace ให้ตรงกับของคุณ
using BuffetAPI.Models; // ⚠️ เช็คชื่อ Namespace ให้ตรงกับของคุณ
using System;
using System.Threading.Tasks;

namespace BuffetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingController : ControllerBase
    {
        private readonly BuffetDbContext _context;

        public BillingController(BuffetDbContext context)
        {
            _context = context;
        }

        // GET: api/Billing/check/SRV001
        [HttpGet("check/{serviceId}")]
        public async Task<ActionResult<BillResult>> CheckBill(string serviceId)
        {
            // 1. ดึงข้อมูล Service พร้อม Package ที่ลูกค้าเลือก
            var service = await _context.Services
                                        .Include(s => s.Package) // Join ตาราง Package
                                        .FirstOrDefaultAsync(s => s.ServiceId == serviceId);

            if (service == null) return NotFound("ไม่พบข้อมูลการเปิดโต๊ะ");
            if (service.Package == null) return BadRequest("ไม่พบข้อมูลแพ็กเกจ (Package Missing)");

            // 2. คำนวณเงิน
            // 1. ดึงจำนวนคน (แปลง int? ให้เป็น int ปกติก่อน)
            int people = service.ServiceNumberPeople ?? 1;

            // 2. ดึงราคา (แปลงเป็น decimal)
            decimal price = (decimal)(service.Package.Package_Price ?? 0);

            // 3. คำนวณ
            decimal total = price * people;
            decimal vat = total * 0.07m;
            decimal grandTotal = total + vat;

            // 3. ยัดลงกล่อง BillResult ส่งกลับไป
            var bill = new BillResult
            {
                ServiceId = service.ServiceId,
                Package_Name = service.Package.Package_Name,
                TotalPeople = people,
                Package_Price = price,
                TotalAmount = total,
                VatAmount = vat,
                GrandTotal = grandTotal,
                PackageId = service.PackageId,
            };

            return Ok(bill);
        }

        // =======================================================
        // ✅ ส่วนที่เพิ่มใหม่: API สำหรับบันทึกการชำระเงิน
        // =======================================================
        [HttpPost("pay")]
        public async Task<IActionResult> SavePayment([FromBody] PaymentRequestDTO request)
        {
            try
            {
                // สร้าง Object ของ Model Payment (ตารางชำระเงิน)
                var newPayment = new Payment
                {
                    // สร้างรหัส Pay_id อัตโนมัติ ความยาวไม่เกิน 10 ตัวอักษร เช่น "P260303123"
                    Pay_id = DateTime.Now.ToString("yyMMddHHm"),

                    // เชื่อมฟีลด์ให้ตรงกัน
                    Pay_forkey = request.Promo_ID, // รหัสโปรโมชั่น
                    Pay_mem = request.MemberId,    // รหัสสมาชิก
                    Pay_amont = (int)request.TotalAmount, // แปลงจาก decimal เป็น int ตามที่ Model กำหนด

                    // แยกเก็บวันที่ และ เวลา
                    Pay_date = DateTime.Now.Date,
                    Pay_time = DateTime.Now.TimeOfDay,

                    Pay_channel = request.PaymentMethod // ช่องทางการชำระเงิน
                };

                // นำข้อมูลลง Database
                _context.Payments.Add(newPayment);
                await _context.SaveChangesAsync();

                return Ok(new { message = "บันทึกการชำระเงินสำเร็จ" });
            }
            catch (Exception ex)
            {
                // ✅ เพิ่มโค้ดบรรทัดนี้: เพื่อดึงสาเหตุที่ลึกที่สุด (InnerException) จาก SQL Server ออกมา
                string innerError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;

                return StatusCode(500, $"เซิร์ฟเวอร์แจ้งสาเหตุว่า:\n{innerError}");
            }
        }
    }

    // =======================================================
    // ✅ ส่วนที่เพิ่มใหม่: คลาสกล่องรับข้อมูล (DTO) วางไว้นอกคลาส Controller
    // =======================================================
    public class PaymentRequestDTO
    {
        public string ServiceId { get; set; } = null!;
        public decimal TotalAmount { get; set; }
        public string? Promo_ID { get; set; }
        public int? MemberId { get; set; }
        public string? PaymentMethod { get; set; }
    }
}