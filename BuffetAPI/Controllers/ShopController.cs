using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BuffetAPI.Data;
using BuffetAPI.Models;

namespace BuffetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly BuffetDbContext _context;

        public ShopController(BuffetDbContext context)
        {
            _context = context;
        }

        // 1. ดูข้อมูลร้านค้าทั้งหมด (GET: api/Shop)
        [HttpGet]
        public async Task<IActionResult> GetShops()
        {
            var shops = await _context.Shops.ToListAsync();
            return Ok(shops);
        }

        // 2. ดูข้อมูลร้านค้าตาม ID (GET: api/Shop/S01)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetShop(string id)
        {
            var shop = await _context.Shops.FindAsync(id);
            if (shop == null) return NotFound(new { message = "ไม่พบข้อมูลร้านค้า" });

            return Ok(shop);
        }

        // 3. เพิ่มร้านค้าใหม่ (POST: api/Shop)
        [HttpPost]
        public async Task<IActionResult> CreateShop([FromBody] Shop shop)
        {
            // เช็คว่ารหัสซ้ำไหม
            if (await _context.Shops.AnyAsync(s => s.ShopId == shop.ShopId))
            {
                return BadRequest(new { message = "รหัสร้านค้านี้มีในระบบแล้ว" });
            }

            _context.Shops.Add(shop);
            await _context.SaveChangesAsync();
            return Ok(new { success = true, message = "เพิ่มข้อมูลร้านค้าสำเร็จ" });
        }

        // 4. แก้ไขข้อมูลร้านค้า (PUT: api/Shop/S01)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShop(string id, [FromBody] Shop shop)
        {
            if (id != shop.ShopId) return BadRequest(new { message = "รหัสร้านค้าไม่ตรงกัน" });

            _context.Entry(shop).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { success = true, message = "อัปเดตข้อมูลสำเร็จ" });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Shops.Any(e => e.ShopId == id)) return NotFound();
                else throw;
            }
        }

        // 5. ลบข้อมูลร้านค้า (DELETE: api/Shop/S01)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShop(string id)
        {
            var shop = await _context.Shops.FindAsync(id);
            if (shop == null) return NotFound(new { message = "ไม่พบข้อมูลที่ต้องการลบ" });

            _context.Shops.Remove(shop);
            await _context.SaveChangesAsync();
            return Ok(new { success = true, message = "ลบข้อมูลสำเร็จ" });
        }
    }
}