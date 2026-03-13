using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BuffetAPI.Data;   // เปลี่ยนให้ตรงกับ Namespace ของคุณ
using BuffetAPI.Models; // เปลี่ยนให้ตรงกับ Namespace ของคุณ

namespace BuffetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly BuffetDbContext _context;

        public EmployeeController(BuffetDbContext context)
        {
            _context = context;
        }

        // 1. ดึงข้อมูลพนักงานทั้งหมด (GET: api/Employee)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            // ดึงข้อมูลพนักงาน (ถ้าอยากให้ดึงข้อมูลร้านค้ามาแสดงด้วย ให้เพิ่ม .Include(e => e.Shop))
            return await _context.Employees.ToListAsync();
        }

        // 2. ดึงข้อมูลพนักงานตามรหัส (GET: api/Employee/E01)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(string id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
                return NotFound(new { message = "ไม่พบข้อมูลพนักงานรายนี้" });

            return Ok(employee);
        }

        // 3. เพิ่มพนักงานใหม่ (POST: api/Employee)
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] Employee employee)
        {
            // เช็คว่ารหัสพนักงานซ้ำไหม
            if (await _context.Employees.AnyAsync(e => e.EmployId == employee.EmployId))
            {
                return BadRequest(new { message = "รหัสพนักงานนี้มีในระบบแล้ว" });
            }

            // (ทางเลือก) เช็คว่า ShopId ที่ส่งมา มีอยู่จริงในตาราง Shop ไหม
            if (!string.IsNullOrEmpty(employee.ShopId))
            {
                var shopExists = await _context.Shops.AnyAsync(s => s.ShopId == employee.ShopId);
                if (!shopExists)
                {
                    return BadRequest(new { message = "ไม่พบรหัสร้านค้านี้ในระบบ กรุณาตรวจสอบ ShopId" });
                }
            }

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return Ok(new { success = true, message = "เพิ่มข้อมูลพนักงานสำเร็จ" });
        }

        // 4. แก้ไขข้อมูลพนักงาน (PUT: api/Employee/E01)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(string id, [FromBody] Employee employee)
        {
            if (id != employee.EmployId)
                return BadRequest(new { message = "รหัสพนักงานไม่ตรงกัน" });

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { success = true, message = "อัปเดตข้อมูลพนักงานสำเร็จ" });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Employees.Any(e => e.EmployId == id))
                    return NotFound(new { message = "ไม่พบข้อมูลพนักงานที่ต้องการแก้ไข" });
                else
                    throw;
            }
        }

        // 5. ลบพนักงาน (DELETE: api/Employee/E01)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
                return NotFound(new { message = "ไม่พบข้อมูลพนักงานที่ต้องการลบ" });

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return Ok(new { success = true, message = "ลบข้อมูลพนักงานสำเร็จ" });
        }
    }
}