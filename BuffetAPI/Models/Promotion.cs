using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuffetAPI.Models
{
    [Table("Promotion")]
    public partial class Promotion
    {
        [Key]
        [MaxLength(10)]
        public string Promo_ID { get; set; } = null!;

        [MaxLength(10)]
        public string? Package_ID { get; set; } // FK

        [MaxLength(50)]
        public string? Promo_Name { get; set; }

        public int? Promo_Discount { get; set; }

        [ForeignKey("Package_ID")]
        public virtual Package? Package { get; set; }
    }
}
