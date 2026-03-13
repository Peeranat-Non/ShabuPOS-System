using Microsoft.AspNetCore.Mvc;
using BuffetAPI.Models;
using Microsoft.EntityFrameworkCore;
using BuffetAPI.Data;

namespace BuffetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly BuffetDbContext _context;

        public MenuController(BuffetDbContext context)
        {
            _context = context;
        }

        // 1. ดึงข้อมูลเมนูทั้งหมด (GET: api/Menu) - ของเดิมของคุณ
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Menu>>> GetMenus()
        {
            return await _context.Menus.ToListAsync();
        }

        // 2. ดึงข้อมูลเมนูตาม ID (GET: api/Menu/M01)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenu(string id)
        {
            var menu = await _context.Menus.FindAsync(id);
            if (menu == null) return NotFound(new { message = "ไม่พบรายการอาหารนี้" });

            return Ok(menu);
        }

        // 3. เพิ่มรายการอาหารใหม่ (POST: api/Menu)
        [HttpPost]
        public async Task<IActionResult> CreateMenu([FromBody] Menu menu)
        {
            // เช็คว่ารหัสซ้ำไหม (เช่น ห้ามมี M01 ซ้ำกัน)
            if (await _context.Menus.AnyAsync(m => m.MenuId == menu.MenuId))
            {
                return BadRequest(new { message = "รหัสเมนูนี้มีในระบบแล้ว" });
            }

            _context.Menus.Add(menu);
            await _context.SaveChangesAsync();
            return Ok(new { success = true, message = "เพิ่มรายการอาหารสำเร็จ" });
        }

        // 4. แก้ไขข้อมูลอาหาร (PUT: api/Menu/M01)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMenu(string id, [FromBody] Menu menu)
        {
            if (id != menu.MenuId) return BadRequest(new { message = "รหัสเมนูไม่ตรงกัน" });

            _context.Entry(menu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { success = true, message = "อัปเดตรายการอาหารสำเร็จ" });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Menus.Any(e => e.MenuId == id)) return NotFound();
                else throw;
            }
        }

        // 5. ลบรายการอาหาร (DELETE: api/Menu/M01)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenu(string id)
        {
            var menu = await _context.Menus.FindAsync(id);
            if (menu == null) return NotFound(new { message = "ไม่พบรายการอาหารที่ต้องการลบ" });

            _context.Menus.Remove(menu);
            await _context.SaveChangesAsync();
            return Ok(new { success = true, message = "ลบรายการอาหารสำเร็จ" });
        }
    }
}