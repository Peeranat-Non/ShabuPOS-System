using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuffetAPI.Models
{
    [Table("Product")]
    public partial class Product
    {
        [Key]
        [MaxLength(10)]
        public string Pro_id { get; set; } = null!;

        [MaxLength(50)]
        public string? Pro_name { get; set; }

        // ✅ เพิ่มฟิลด์หมวดหมู่สินค้าตรงนี้ครับ
        [MaxLength(50)]
        public string? Pro_category { get; set; }

        public int? Pro_quan { get; set; }
        public int? Pro_stock { get; set; }

        [MaxLength(10)]
        public string? Pro_unit { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Pro_price { get; set; }

        [MaxLength(255)]
        public string? Pro_image { get; set; }

        // Navigation Properties กลับไปยังตาราง Detail และ Stock
        public virtual ICollection<PurchaseRequisitionDetail> PurchaseRequisitionDetails { get; set; } = new List<PurchaseRequisitionDetail>();
        public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; } = new List<PurchaseOrderDetail>();
        public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
    }
}