using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuffetAPI.Models
{
    [Table("Income")]
    public partial class Income
    {
        [Key]
        [MaxLength(10)]
        public string Income_id { get; set; } = null!;

        [MaxLength(10)]
        public string? Income_payment { get; set; } // FK

        public DateTime? Income_date { get; set; }
        public TimeSpan? Income_time { get; set; }

        [MaxLength(50)]
        public string? Income_description { get; set; }

        public int? Income_amount { get; set; }

        [ForeignKey("Income_payment")]
        public virtual Payment? Payment { get; set; }
    }
}
