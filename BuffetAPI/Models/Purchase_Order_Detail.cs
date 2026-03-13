using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuffetAPI.Models
{
    [Table("Purchase_Order_Detail")]
    public partial class PurchaseOrderDetail
    {
        [Key]
        public int PO_Detail_ID { get; set; } // รันตัวเลขอัตโนมัติ

        [Required]
        [MaxLength(10)]
        public string Po_id { get; set; } = null!; // FK ชี้ไปที่หัวใบสั่งซื้อ

        [Required]
        [MaxLength(10)]
        public string Pro_id { get; set; } = null!; // FK ชี้ไปที่สินค้า

        public int Order_Qty { get; set; } // จำนวนที่สั่งซื้อ

        [Column(TypeName = "decimal(10, 2)")]
        public decimal Unit_Price { get; set; } // ราคาต่อหน่วย

        [Column(TypeName = "decimal(10, 2)")]
        public decimal Total_Price { get; set; } // ราคารวม (Qty * Unit_Price)

        // Navigation Properties
        [ForeignKey("Po_id")]
        public virtual PurchaseOrder? PurchaseOrder { get; set; }

        [ForeignKey("Pro_id")]
        public virtual Product? Product { get; set; }
    }
}