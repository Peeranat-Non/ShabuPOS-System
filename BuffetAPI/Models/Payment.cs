using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuffetAPI.Models
{
    [Table("Payment")]
    public partial class Payment
    {
        [Key]
        [MaxLength(10)]
        public string Pay_id { get; set; } = null!;

        [MaxLength(10)]
        public string? Pay_forkey { get; set; } // รหัสโปรโมชั่น (FK)

        [MaxLength(10)]
        public int? Pay_mem { get; set; } // FK

        public DateTime? Pay_date { get; set; }
        public TimeSpan? Pay_time { get; set; }
        public int? Pay_amont { get; set; }

        [MaxLength(5)]
        public string? Pay_channel { get; set; }

        [ForeignKey("Pay_forkey")]
        public virtual Promotion? Promotion { get; set; }

        [ForeignKey("Pay_mem")]
        public virtual Member? Member { get; set; }
    }
}
