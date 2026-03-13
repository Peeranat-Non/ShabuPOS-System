using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuffetAPI.Models
{
    [Table("Purchase_Requisition")] // อิงจากตาราง PR
    public partial class PurchaseRequisition
    {
        [Key]
        [MaxLength(10)]
        public string PR_ID { get; set; } = null!;

        [MaxLength(10)]
        public string? Employ_ID { get; set; } // ใน Data Dic พิมพ์ชื่อฟิลด์ซ้ำเป็น PR_ID แต่ชี้ไปที่ Employ ขอแก้ให้ตรงตามความหมายฃฃ

        // ✅ เพิ่ม: แผนกที่ขอซื้อ (ครัวร้อน, บาร์น้ำ, ของสด) เพื่อใช้ในตัวกรอง [cite: 2026-03-08]
        [MaxLength(50)]
        public string? PR_Department { get; set; }

        public DateTime PR_Date { get; set; } = DateTime.Now;

        [MaxLength(50)]
        public string? PR_Status { get; set; } = "Pending"; // ค่าเริ่มต้นเป็น 'รออนุมัติ' [cite: 2026-03-08]

        [ForeignKey("Employ_ID")]
        public virtual Employee? Employee { get; set; }

        // เติมบรรทัดนี้ไว้ล่างสุดของคลาส
        public virtual ICollection<PurchaseRequisitionDetail> PurchaseRequisitionDetails { get; set; } = new List<PurchaseRequisitionDetail>();
    }
}
