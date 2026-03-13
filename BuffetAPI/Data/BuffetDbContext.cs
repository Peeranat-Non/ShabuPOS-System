using Microsoft.EntityFrameworkCore;
using BuffetAPI.Models;

namespace BuffetAPI.Data
{
    public class BuffetDbContext : DbContext
    {
        public BuffetDbContext()
        {
        }

        public BuffetDbContext(DbContextOptions<BuffetDbContext> options)
            : base(options)
        {
        }

        // ประกาศตารางต่างๆ
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<OrderHeader> OrderHeaders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<Shop> Shops { get; set; }
        public virtual DbSet<Package> Packages { get; set; }
        public virtual DbSet<Promotion> Promotion { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Income> Incomes { get; set; }
        public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; } // ตารางใหม่
        public virtual DbSet<PurchaseRequisition> PurchaseRequisitions { get; set; }
        public virtual DbSet<PurchaseRequisitionDetail> PurchaseRequisitionDetails { get; set; } // ตารางใหม่
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Expense> Expenses { get; set; }
        public virtual DbSet<Stock> Stocks { get; set; }
        public virtual DbSet<Fee> Fees { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        // ใช้ Database ชื่อเดิม (BuffetDB) ตามที่คุณตั้งไว้
        //        optionsBuilder.UseSqlServer("Server=NONNY;Database=BuffetDB;Trusted_Connection=True;TrustServerCertificate=True;");
        //    }
        //}

        // 🌟 ท่าไม้ตาย: เพิ่ม OnConfiguring กลับเข้ามา เพื่อเป็น Safety Net ให้ WinForms
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // เช็คว่า "ถ้ายังไม่มีใครตั้งค่า Connection String มาให้ (IsConfigured == false)"
            if (!optionsBuilder.IsConfigured)
            {
                // ให้ใช้ค่า Default นี้ไปเลย (WinForms จะวิ่งมารับค่านี้ไปใช้โดยอัตโนมัติ)
                optionsBuilder.UseSqlServer("Server=NONNY;Database=BuffetDB;Integrated Security=True;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =========================================================
            // ✅ 1. ตั้งค่าความสัมพันธ์: ใบขอซื้อ (Purchase Requisition)
            // =========================================================
            modelBuilder.Entity<PurchaseRequisitionDetail>()
                .HasOne(d => d.PurchaseRequisition)
                .WithMany(p => p.PurchaseRequisitionDetails)
                .HasForeignKey(d => d.PR_ID)
                .OnDelete(DeleteBehavior.Cascade); // ถ้าลบหัวบิล PR ให้ลบรายการย่อยทิ้งด้วยอัตโนมัติ

            modelBuilder.Entity<PurchaseRequisitionDetail>()
                .HasOne(d => d.Product)
                .WithMany(p => p.PurchaseRequisitionDetails)
                .HasForeignKey(d => d.Pro_id)
                .OnDelete(DeleteBehavior.Restrict); // ป้องกันการลบสินค้า ถ้าสินค้านั้นถูกขอซื้อไปแล้ว

            // =========================================================
            // ✅ 2. ตั้งค่าความสัมพันธ์: ใบสั่งซื้อ (Purchase Order)
            // =========================================================
            modelBuilder.Entity<PurchaseOrderDetail>()
                .HasOne(d => d.PurchaseOrder)
                .WithMany(p => p.PurchaseOrderDetails)
                .HasForeignKey(d => d.Po_id)
                .OnDelete(DeleteBehavior.Cascade); // ถ้าลบหัวบิล PO ให้ลบรายการย่อยทิ้งด้วยอัตโนมัติ

            modelBuilder.Entity<PurchaseOrderDetail>()
                .HasOne(d => d.Product)
                .WithMany(p => p.PurchaseOrderDetails)
                .HasForeignKey(d => d.Pro_id)
                .OnDelete(DeleteBehavior.Restrict); // ป้องกันการลบสินค้า ถ้าสินค้านั้นถูกสั่งซื้อไปแล้ว

            // =========================================================
            // ✅ 3. ตั้งค่าความสัมพันธ์: สต๊อก (Stock)
            // =========================================================
            modelBuilder.Entity<Stock>()
                .HasOne(s => s.Product)
                .WithMany(p => p.Stocks)
                .HasForeignKey(s => s.Pro_id)
                .OnDelete(DeleteBehavior.Restrict); // ป้องกันการลบสินค้า ถ้าเคยมีการเคลื่อนไหวสต๊อกแล้ว

        }
    }
}