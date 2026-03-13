using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuffetAPI.Models
{
    [Table("Expense")]
    public partial class Expense
    {
        [Key]
        [MaxLength(10)]
        public string Exp_id { get; set; } = null!; // ✅ เปลี่ยนกลับเป็น string ตาม Error ที่ระบุ

        public DateTime? Exp_date { get; set; }
        public TimeSpan? Exp_time { get; set; }
        public decimal? Exp_amount { get; set; }
    }
}
