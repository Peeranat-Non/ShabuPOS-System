using Dapper; // ✅ บรรทัดนี้จะทำให้ QueryAsync ทำงานได้
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration; // ✅ เพิ่มบรรทัดนี้เพื่อใช้งาน IConfiguration

namespace BuffetAPI.Controllers // ใส่ namespace ให้ตรงกับโปรเจกต์ของคุณ (ถ้ามี)
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        // ✅ ประกาศตัวแปรแบบไม่ต้องกำหนดค่าตายตัว
        private readonly string _connectionString;

        // ✅ เพิ่ม Constructor เพื่อรับค่า IConfiguration จากระบบ (Dependency Injection)
        public ProductsController(IConfiguration configuration)
        {
            // ✅ ดึงค่า "DefaultConnection" มาจากไฟล์ appsettings.json
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string category, string search)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                // ✅ ต้องใช้ชื่อคอลัมน์ให้ตรงกับในตาราง Product (Pro_name, Pro_category)
                string sql = @"
                SELECT 
                    Pro_id AS Id, 
                    Pro_name AS Name, 
                    Pro_stock AS Stock, 
                    Pro_unit AS Unit,
                    Pro_category AS Category
                FROM Product 
                WHERE (Pro_name LIKE @keyword OR @keyword IS NULL)
                AND (Pro_category = @cat OR @cat = 'ทั้งหมด')";

                var parameters = new
                {
                    keyword = string.IsNullOrEmpty(search) ? null : "%" + search + "%",
                    cat = category
                };

                // ✅ ใช้ ProductGridDisplay เพื่อให้ชื่อ Property ตรงกับตารางที่ WinForms รอรับ
                var results = await conn.QueryAsync<ProductGridDisplay>(sql, parameters);
                return Ok(results);
            }
        }

        // ✅ แก้ไข Data Type ให้ตรงกับ Database (Id ต้องเป็น string ไม่ใช่ int)
        public class ProductGridDisplay
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public int Stock { get; set; }
            public string Unit { get; set; }
            public string Category { get; set; }
        }
    }
}