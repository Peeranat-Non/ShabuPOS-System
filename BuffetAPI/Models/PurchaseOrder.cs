using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuffetAPI.Models
{
    [Table("Purchase_Order")] // อิงจากตาราง PO
    public partial class PurchaseOrder
    {
        [Key]
        [MaxLength(10)]
        public string Po_id { get; set; } = null!;

        public DateTime? Po_date { get; set; }
        public TimeSpan? Po_time { get; set; }

        [MaxLength(10)]
        public string? Po_employee { get; set; } // FK

        [MaxLength(50)]
        public string? Po_status { get; set; }

        [MaxLength(50)]
        public string? Po_approver { get; set; }

        [MaxLength(10)]
        public string? Po_Buyreq { get; set; } // น่าจะเป็น FK ไปหา PR_ID

        [ForeignKey("Po_employee")]
        public virtual Employee? Employee { get; set; }

        // เติมบรรทัดนี้ไว้ล่างสุดของคลาส
        public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; } = new List<PurchaseOrderDetail>();
    }
}
