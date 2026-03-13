using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuffetAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace ShabuPOS
{
    public static class DbConfig
    {
        // 🌟 เก็บ Connection String ไว้ที่นี่ที่เดียว
        public static readonly string ConnectionString = @"Server=NONNY;Database=BuffetDB;Integrated Security=True;TrustServerCertificate=True;";

        // 🌟 เพิ่มเมธอดนี้เข้าไปใหม่ 🌟
        public static BuffetDbContext GetDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<BuffetDbContext>();
            optionsBuilder.UseSqlServer(ConnectionString);

            // ส่งค่า Options กลับไปให้พร้อมใช้งาน
            return new BuffetDbContext(optionsBuilder.Options);
        }
    }
}
