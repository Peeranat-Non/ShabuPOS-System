using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuffetAPI.Models
{
    [Table("fee")]
    public partial class Fee
    {
        [Key]
        [MaxLength(10)]
        public string fee_id { get; set; } = null!;

        [MaxLength(10)]
        public string? Exp_id { get; set; } // FK

        public DateTime? fee_date { get; set; }
        public TimeSpan? fee_time { get; set; }
        public decimal? fee_total { get; set; }

        [ForeignKey("Exp_id")]
        public virtual Expense? Expense { get; set; }
    }
}
