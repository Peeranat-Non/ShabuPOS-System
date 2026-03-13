    using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuffetAPI.Models
{
    [Table("Package")]
    public partial class Package
    {
        [Key]
        [MaxLength(10)]
        public string Package_ID { get; set; } = null!;

        [MaxLength(10)]
        public string? Service_ID { get; set; } // FK อิงจาก "รหัสการใช้บริการ"

        [MaxLength(50)]
        public string? Package_Name { get; set; }

        public int? Package_Price { get; set; }

        [ForeignKey("Service_ID")]
        public virtual Service? Service { get; set; }

        public virtual ICollection<Menu> Menus { get; set; } = new List<Menu>();
    }
}
