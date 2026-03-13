using Microsoft.AspNetCore.Mvc;
using BuffetAPI.Models;
using Microsoft.EntityFrameworkCore;
using BuffetAPI.Data;

namespace BuffetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly BuffetDbContext _context;

        public OrderController(BuffetDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] OrderRequest request)
        {
            if (request.OrderDetails == null || request.OrderDetails.Count == 0)
            {
                return BadRequest("ไม่พบรายการอาหารในออเดอร์");
            }

            // เริ่มต้น Transaction เพื่อความปลอดภัยของข้อมูล
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // 1. สร้างข้อมูลส่วน Header (Master) [cite: 2026-02-26]
                var header = new OrderHeader
                {
                    OrderTable = request.OrderTable,
                    ServiceId = request.ServiceId ?? "SRV001", // ใช้ค่าจากหน้าเว็บหรือ Hardcode เทส [cite: 2026-03-07]
                    OrderDate = DateOnly.FromDateTime(DateTime.Now),
                    OrderTime = TimeOnly.FromDateTime(DateTime.Now),
                    TotalStatus = "Pending" // สถานะภาพรวมของบิล [cite: 2026-02-26]
                };

                _context.OrderHeaders.Add(header);
                await _context.SaveChangesAsync(); // บันทึกเพื่อให้ได้ OrderHeaderId มาใช้งาน [cite: 2026-02-26]

                // 2. วนลูปสร้างรายการอาหาร (Details) โดยผูกกับ Header ID [cite: 2026-03-07]
                foreach (var item in request.OrderDetails)
                {
                    var detail = new OrderDetail
                    {
                        OrderHeaderId = header.OrderHeaderId, // FK เชื่อมกลับไปหาใบสั่งซื้อ [cite: 2026-03-07]
                        MenuId = item.MenuId,
                        OrderQuantity = item.OrderQuantity,
                        ItemStatus = "Waiting" // สถานะรายจานสำหรับพนักงานครัว [cite: 2026-03-07]
                    };

                    _context.OrderDetails.Add(detail);
                }

                await _context.SaveChangesAsync();

                // ยืนยันการบันทึกข้อมูลทั้งหมด [cite: 2025-11-23]
                await transaction.CommitAsync();

                return Ok(new
                {
                    message = "ส่งออเดอร์ไปยังครัวเรียบร้อยแล้ว!",
                    orderId = header.OrderHeaderId
                });
            }
            catch (Exception ex)
            {
                // ถ้าพลาดให้ยกเลิกสิ่งที่ทำมาทั้งหมด [cite: 2025-11-23]
                await transaction.RollbackAsync();
                return StatusCode(500, $"เกิดข้อผิดพลาด: {ex.Message}");
            }
        }
    }

    // ✅ ปรับ DTO ให้ตรงกับที่หน้าเว็บส่งมาล่าสุด [cite: 2026-03-07]
    public class OrderRequest
    {
        public string OrderTable { get; set; }
        public string? ServiceId { get; set; }
        public List<OrderDetailRequest> OrderDetails { get; set; }
    }

    public class OrderDetailRequest
    {
        public string MenuId { get; set; }
        public int OrderQuantity { get; set; }
    }
}