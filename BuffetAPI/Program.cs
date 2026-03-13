using BuffetAPI.Data;
using BuffetAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. ตั้งค่า CORS (อนุญาตให้ Frontend เข้าถึง API ได้)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowNextJs",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});


builder.Services.AddDbContext<BuffetDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// ใช้งาน Middleware
app.UseCors("AllowNextJs");

// =========================================================
// 🟢 ส่วนการ Seed Data (ลำดับความสัมพันธ์ของตาราง)
// =========================================================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<BuffetDbContext>();

        // ตรวจสอบความถูกต้องของตารางก่อนทำงาน
        context.Database.Migrate();

        // --- 1. Shop (ตารางราก) ---
        if (!context.Shops.Any())
        {
            Console.WriteLine("Seeding Shop Data...");
            context.Shops.Add(new Shop { ShopId = "S001", ShopName = "Shabu Noony", ShopAddress = "Bangkok", ShopPhone = "0812345678" });
            context.SaveChanges();
        }

        // --- 2. Package (ตารางแม่สำหรับ Service) ---
        if (!context.Packages.Any())
        {
            Console.WriteLine("Seeding Package Data...");
            context.Packages.AddRange(
                new Package { Package_ID = "PACK001", Package_Name = "Silver", Package_Price = 299 },
                new Package { Package_ID = "PACK002", Package_Name = "Gold", Package_Price = 399 },
                new Package { Package_ID = "PACK003", Package_Name = "Platinum", Package_Price = 499 }
            );
            context.SaveChanges(); // 👈 ต้อง Save ทันที เพื่อให้รหัสรันในระบบ
        }

        // --- 3. Employee (ตารางแม่สำหรับ Service) ---
        if (!context.Employees.Any())
        {
            Console.WriteLine("Seeding Employee Data...");
            context.Employees.Add(new Employee
            {
                EmployId = "E001",
                EmployName = "Admin",
                EmployPosition = "Manager",
                ShopId = "S001",
                EmploySdate = DateOnly.FromDateTime(DateTime.Now)
            });
            context.SaveChanges();
        }

        // --- 4. Menu ---
        if (!context.Menus.Any())
        {
            Console.WriteLine("Seeding Menu Data...");
            context.Menus.AddRange(
                new Menu { MenuId = "M001", MenuName = "หมูสไลด์", MenuPrice = 0, MenuImage = "/images/sliced-pork.jpg" },
                new Menu { MenuId = "M002", MenuName = "เนื้อวากิว", MenuPrice = 19, MenuImage = "/images/wagyu.jpg" },
                new Menu { MenuId = "M003", MenuName = "ผักรวม", MenuPrice = 0, MenuImage = "/images/veg.png" },
                new Menu { MenuId = "M004", MenuName = "น้ำจิ้มสุกี้", MenuPrice = 0, MenuImage = "/images/sauce.jpg" },
                new Menu { MenuId = "M005", MenuName = "เบคอน", MenuPrice = 0, MenuImage = "/images/bacon.jpg" },
                new Menu { MenuId = "M006", MenuName = "เนื้อแองกัส", MenuPrice = 0, MenuImage = "/images/angus-beef.jpg" },
                new Menu { MenuId = "M007", MenuName = "ปลาหมึก", MenuPrice = 0, MenuImage = "/images/squid.jpg" },
                new Menu { MenuId = "M008", MenuName = "กุ้งแม่น้ำ", MenuPrice = 0, MenuImage = "/images/shrimp.jpg" },
                new Menu { MenuId = "M009", MenuName = "ข้าวโพดหวาน", MenuPrice = 0, MenuImage = "/images/corn.jpg" },
                new Menu { MenuId = "M010", MenuName = "เต้าหู้ไข่", MenuPrice = 0, MenuImage = "/images/tofu.jpg" },
                new Menu { MenuId = "M011", MenuName = "ปอเปี๊ยะทอด", MenuPrice = 0, MenuImage = "/images/gyoza.jpg" },
                new Menu { MenuId = "M012", MenuName = "เกี๊ยวซ่า", MenuPrice = 0, MenuImage = "/images/spring-rolls.jpg" },
                new Menu { MenuId = "M013", MenuName = "ถั่วแระญี่ปุ่น", MenuPrice = 0, MenuImage = "/images/edamame.jpg" },
                new Menu { MenuId = "M014", MenuName = "ชาเขียว", MenuPrice = 0, MenuImage = "/images/green-tea.jpg" },
                new Menu { MenuId = "M015", MenuName = "โค้ก", MenuPrice = 0, MenuImage = "/images/coke.jpg" },
                new Menu { MenuId = "M016", MenuName = "น้ำเปล่า", MenuPrice = 0, MenuImage = "/images/water.jpg" },
                new Menu { MenuId = "M017", MenuName = "น้ำจิ้มสุกี้ สูตร 2", MenuPrice = 0, MenuImage = "/images/Suki.jpg" }
            );
            context.SaveChanges();
        }

        // --- 5. Product (คลังสินค้า) ---
        if (!context.Products.Any())
        {
            Console.WriteLine("Seeding Product Data...");
            context.Products.AddRange(
                new Product { Pro_id = "P001", Pro_name = "หมูสไลด์ (สต๊อก)", Pro_category = "เนื้อสัตว์", Pro_quan = 10, Pro_stock = 50, Pro_unit = "กก.", Pro_price = 150.00m },
                new Product { Pro_id = "P002", Pro_name = "เนื้อวากิว (สต๊อก)", Pro_category = "เนื้อสัตว์", Pro_quan = 10, Pro_stock = 50, Pro_unit = "กก.", Pro_price = 450.00m },
                new Product { Pro_id = "P003", Pro_name = "ผักรวม (สต๊อก)", Pro_category = "ผัก", Pro_quan = 10, Pro_stock = 50, Pro_unit = "แพ็ค", Pro_price = 50.00m },
                new Product { Pro_id = "P004", Pro_name = "น้ำจิ้มสุกี้ (สต๊อก)", Pro_category = "เครื่องปรุง", Pro_quan = 10, Pro_stock = 50, Pro_unit = "แกลลอน", Pro_price = 200.00m },
                new Product { Pro_id = "P005", Pro_name = "เบคอน (สต๊อก)", Pro_category = "เนื้อสัตว์", Pro_quan = 10, Pro_stock = 50, Pro_unit = "กก.", Pro_price = 180.00m },
                new Product { Pro_id = "P014", Pro_name = "ชาเขียว (สต๊อก)", Pro_category = "เครื่องดื่ม", Pro_quan = 10, Pro_stock = 50, Pro_unit = "ขวด", Pro_price = 20.00m }
            );
            context.SaveChanges();
        }

        // --- 6. Service (ตารางลูก - ต้องรอลูกค้าสั่ง) ---
        if (!context.Services.Any())
        {
            // ตรวจสอบความปลอดภัยของความสัมพันธ์ (Foreign Key Check)
            var packageExists = context.Packages.Any(p => p.Package_ID == "PACK001");
            var employeeExists = context.Employees.Any(e => e.EmployId == "E001");

            if (packageExists && employeeExists)
            {
                Console.WriteLine("Seeding Service Data...");
                context.Services.Add(new Service
                {
                    ServiceId = "SRV001",
                    EmployId = "E001",
                    PackageId = "PACK001",
                    ServiceDate = DateOnly.FromDateTime(DateTime.Now),
                    ServiceTime = TimeOnly.FromDateTime(DateTime.Now),
                    ServiceNumberPeople = 2
                });
                context.SaveChanges();
            }
        }

        Console.WriteLine("✅ All Seed Data Completed!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Seed Error: {ex.Message}");
        if (ex.InnerException != null) Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
    }
}
// =========================================================

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();