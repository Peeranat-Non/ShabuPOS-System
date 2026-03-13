using Microsoft.AspNetCore.Mvc;
using BuffetAPI.Data;
using BuffetAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BuffetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly BuffetDbContext _context;

        public PromotionController(BuffetDbContext context)
        {
            _context = context;
        }

        // ==========================================
        // ✅ คำสั่งสำหรับ "เพิ่มโปรโมชั่นใหม่" (POST)
        // ==========================================
        [HttpPost]
        public async Task<IActionResult> CreatePromotion([FromBody] Promotion promo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // นำโปรโมชั่นใหม่แอดลงฐานข้อมูล
                _context.Promotion.Add(promo);
                await _context.SaveChangesAsync();

                return Ok(new { success = true, message = "บันทึกโปรโมชั่นใหม่สำเร็จ!" });
            }
            catch (Exception ex)
            {
                // ดักจับ Error ลึกๆ เผื่อรหัสซ้ำ หรือข้อมูลไม่ครบ
                string innerError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return StatusCode(500, $"เซิร์ฟเวอร์แจ้งสาเหตุว่า:\n{innerError}");
            }
        }
        // ==========================================
        // ✅ คำสั่งสำหรับ "ดึงโปรโมชั่นทั้งหมด" (GET)
        // ==========================================
        [HttpGet]
        public async Task<IActionResult> GetAllPromotions()
        {
            try
            {
                var promotions = await _context.Promotion.ToListAsync();
                return Ok(promotions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        // ==========================================
        // ✅ คำสั่งสำหรับ "ลบโปรโมชั่น" (DELETE)
        // ==========================================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePromotion(string id)
        {
            try
            {
                // 1. ค้นหาโปรโมชั่นจากรหัส (Promo_ID)
                var promo = await _context.Promotion.FirstOrDefaultAsync(p => p.Promo_ID == id);

                if (promo == null)
                {
                    return NotFound(new { message = "ไม่พบรหัสโปรโมชั่นนี้ในระบบ" });
                }

                // 2. สั่งลบและเซฟลงฐานข้อมูล
                _context.Promotion.Remove(promo);
                await _context.SaveChangesAsync();

                return Ok(new { success = true, message = "ลบโปรโมชั่นเรียบร้อยแล้ว" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"เกิดข้อผิดพลาด: {ex.Message}");
            }
        }
        // ==========================================
        // ✅ คำสั่งสำหรับ "แก้ไขข้อมูลโปรโมชั่น" (PUT)
        // ==========================================
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePromotion(string id, [FromBody] Promotion promo)
        {
            // 1. เช็คว่ารหัสที่ส่งมาตรงกันไหม
            if (id != promo.Promo_ID)
            {
                return BadRequest(new { message = "รหัสโปรโมชั่นไม่ตรงกัน" });
            }

            // 2. บอก Entity Framework ว่าข้อมูลชุดนี้มีการแก้ไขนะ
            _context.Entry(promo).State = EntityState.Modified;

            try
            {
                // 3. สั่งเซฟลง Database
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // เช็คว่ามีข้อมูลนี้อยู่จริงไหม ถ้าไม่มีให้ฟ้อง Error
                if (!_context.Promotion.Any(e => e.Promo_ID == id))
                {
                    return NotFound(new { message = "ไม่พบโปรโมชั่นที่ต้องการแก้ไข" });
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { success = true, message = "แก้ไขโปรโมชั่นเรียบร้อย" });
        }
        // ==========================================
        // ✅ คำสั่งสำหรับ "ดึงโปรโมชั่นตามรหัสแพ็กเกจ" (GET)
        // ==========================================
        [HttpGet("package/{packageId}")]
        public async Task<IActionResult> GetPromotionsByPackage(string packageId)
        {
            try
            {
                // ค้นหาเฉพาะโปรโมชั่นที่ตรงกับรหัสแพ็กเกจที่ส่งมา
                var promotions = await _context.Promotion
                                               .Where(p => p.Package_ID == packageId)
                                               .ToListAsync();

                return Ok(promotions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // (โค้ดดึงข้อมูล GET /api/Promotion/package/... ของเดิมที่คุณเฟิร์สมีอยู่แล้ว ปล่อยไว้เหมือนเดิมได้เลยครับ)
    }
}