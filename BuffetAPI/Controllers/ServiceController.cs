using System;
using Microsoft.AspNetCore.Mvc;
using BuffetAPI.Data;
using BuffetAPI.Models;

namespace BuffetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly BuffetDbContext _context;

        public ServiceController(BuffetDbContext context)
        {
            _context = context;
        }

        // POST: api/Service/open
        // API สำหรับเปิดโต๊ะ: รับเบอร์โต๊ะ, แพ็กเกจที่เลือก, จำนวนคน
        [HttpPost("open")]
        public IActionResult OpenTable([FromBody] OpenTableRequest request)
        {
            try
            {
                if (request == null)
                    return BadRequest("Invalid request body.");

                if (string.IsNullOrWhiteSpace(request.TableNo))
                    return BadRequest("TableNo is required.");

                // 1. แปลงเบอร์โต๊ะ (1 -> SRV001)
                string serviceId = "SRV" + request.TableNo.PadLeft(3, '0');

                // 2. ลบข้อมูลเก่าทิ้ง (ถ้ามี) เพื่อเปิดโต๊ะใหม่
                var oldService = _context.Services.Find(serviceId);
                if (oldService != null)
                {
                    _context.Services.Remove(oldService);
                    _context.SaveChanges(); // ลบของเก่าก่อน
                }

                // 3. สร้าง Service ใหม่
                var newService = new Service
                {
                    ServiceId = serviceId,
                    EmployId = "E001", // (Hardcode ไปก่อน หรือส่งมาจาก Login)
                    PackageId = request.PackageId, // ✅ บันทึกแพ็กเกจที่เลือก
                    ServiceNumberPeople = request.People, // ✅ บันทึกจำนวนคน
                    ServiceDate = DateOnly.FromDateTime(DateTime.Now),
                    ServiceTime = TimeOnly.FromDateTime(DateTime.Now)
                };

                _context.Services.Add(newService);
                _context.SaveChanges();

                return Ok(new { message = "เปิดโต๊ะสำเร็จ", serviceId = serviceId });
            }
            catch (Exception ex)
            {
                // ✅ แก้เป็นแบบนี้: ดึง Error ตัวจริงที่ซ่อนอยู่ข้างในออกมา
                var realError = ex.InnerException?.Message ?? ex.Message;
                return BadRequest("เปิดโต๊ะไม่สำเร็จ: " + realError);
            }
        }

        // GET: api/Service/active
        // ดึงรายชื่อโต๊ะที่กำลังกินอยู่ (ไม่ว่าง)
        [HttpGet("active")]
        public IActionResult GetActiveTables()
        {
            // ดึงเฉพาะเบอร์โต๊ะ จากตาราง Service มา
            // สมมติว่า ServiceId เก็บเป็น "SRV001" เราจะตัดเอาแค่เลข 1
            var activeTables = _context.Services
                .Select(s => s.ServiceId) // ได้ SRV001, SRV005
                .ToList();

            // แปลง SRV001 ให้เป็นเลข 1 (int) เพื่อส่งกลับไปใช้ง่ายๆ
            var tableNumbers = activeTables.Select(id =>
            {
                // ตัดคำว่า "SRV" ออก แล้วแปลงเป็นตัวเลข
                int.TryParse(id.Replace("SRV", ""), out int num);
                return num;
            }).ToList();

            return Ok(tableNumbers); // ส่งกลับเป็น List เช่น [1, 5, 12]
        }

        // DELETE: api/Service/close/SRV001
        // เพิ่ม API สำหรับ "เคลียร์โต๊ะ" (เมื่อจ่ายเงินเสร็จ)
        [HttpDelete("close/{serviceId}")]
        public IActionResult CloseTable(string serviceId)
        {
            var service = _context.Services.Find(serviceId);
            if (service == null) return NotFound();

            // ✅ 1. ลบรายการอาหาร (Order) ของโต๊ะนี้ทิ้งก่อน
            // (ต้องแน่ใจว่าคุณมี DbSet<Order> ใน BuffetDbContext นะครับ)
            // ถ้าชื่อตารางใน Code คือ 'Orders' ให้ใช้แบบนี้:
            var relatedOrders = _context.OrderHeaders.Where(o => o.ServiceId == serviceId);
            _context.OrderHeaders.RemoveRange(relatedOrders);

            // ✅ 2. ค่อยลบ Service
            _context.Services.Remove(service);

            _context.SaveChanges();
            return Ok(new { message = "เคลียร์โต๊ะเรียบร้อย" });
        }
    }

    // Class สำหรับรับค่าจากหน้าบ้าน
    public class OpenTableRequest
    {
        public string TableNo { get; set; }
        public string PackageId { get; set; } // เช่น PACK001, PACK002
        public int People { get; set; }
    }
}