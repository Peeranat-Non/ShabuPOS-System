using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuffetAPI.Models
{
    [Table("Stock")]
    public partial class Stock
    {
        [Key]
        [MaxLength(10)]
        public string Stock_ID { get; set; } = null!;

        [Required]
        [MaxLength(10)]
        public string Pro_id { get; set; } = null!; // ✅ เพิ่ม FK ไปหาสินค้า (สำคัญมาก)

        [MaxLength(10)]
        public string? Exp_id { get; set; }

        [MaxLength(10)]
        public string? Po_id { get; set; }

        public DateTime? Stock_Date { get; set; }
        public TimeSpan? Stock_Time { get; set; }

        [MaxLength(10)]
        public string? Transaction_Type { get; set; } // ✅ เช่น "IN" (รับเข้า), "OUT" (เบิกออก)

        public int? Stock_Qty { get; set; } // ✅ จำนวนที่รับเข้าหรือเบิกออก

        [ForeignKey("Pro_id")]
        public virtual Product? Product { get; set; }

        [ForeignKey("Exp_id")]
        public virtual Expense? Expense { get; set; }

        [ForeignKey("Po_id")]
        public virtual PurchaseOrder? PurchaseOrder { get; set; }
    }
}