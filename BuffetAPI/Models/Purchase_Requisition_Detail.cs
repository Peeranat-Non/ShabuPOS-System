using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuffetAPI.Models
{
    [Table("Purchase_Requisition_Detail")]
    public partial class PurchaseRequisitionDetail
    {
        [Key]
        public int PR_Detail_ID { get; set; } // ให้ Database รันตัวเลขให้อัตโนมัติ (Identity)

        [Required]
        [MaxLength(10)]
        public string PR_ID { get; set; } = null!; // FK ชี้ไปที่หัวใบขอซื้อ

        [Required]
        [MaxLength(10)]
        public string Pro_id { get; set; } = null!; // FK ชี้ไปที่สินค้า

        public int Request_Qty { get; set; } // จำนวนที่ขอซื้อ

        // ✅ เพิ่ม: หมายเหตุเพิ่มเติม (เช่น ยี่ห้อที่ระบุพิเศษ หรือความเร่งด่วน) [cite: 2026-02-26]
        [MaxLength(255)]
        public string? Remark { get; set; }

        // Navigation Properties
        [ForeignKey("PR_ID")]
        public virtual PurchaseRequisition? PurchaseRequisition { get; set; }

        [ForeignKey("Pro_id")]
        public virtual Product? Product { get; set; }
    }
}