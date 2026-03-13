using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BuffetAPI.Models;

[Table("Menu")]
public partial class Menu
{
    [Key]
    [MaxLength(10)]
    public string MenuId { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string MenuName { get; set; } = null!;

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? MenuPrice { get; set; }

    [MaxLength(255)]
    public string? MenuImage { get; set; }

    // ✅ ส่วนที่เพิ่มใหม่ 1: สถานะเมนู (พร้อมขาย / หมดชั่วคราว) [cite: 2026-03-08]
    [Required]
    [MaxLength(20)]
    public string MenuStatus { get; set; } = "Available"; // ค่าเริ่มต้นคือ Available (พร้อมขาย)

    // ✅ ส่วนที่เพิ่มใหม่ 2: ความสัมพันธ์กับ Package (Many-to-Many)
    // สิ่งนี้จะช่วยให้ 1 เมนู อยู่ได้หลายแพ็กเกจ เช่น หมูสไลด์ อยู่ทั้ง Silver และ Gold
    public virtual ICollection<Package> Packages { get; set; } = new List<Package>();

    [MaxLength(10)]
    public string? Pro_id { get; set; }

    [ForeignKey("Pro_id")]
    public virtual Product? Product { get; set; }

    public virtual ICollection<OrderDetail> OrderDetail { get; set; } = new List<OrderDetail>();
}