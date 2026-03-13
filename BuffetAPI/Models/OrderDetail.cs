using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BuffetAPI.Models;

[Table("OrderDetail")]
public partial class OrderDetail
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int DetailId { get; set; }

    public int OrderHeaderId { get; set; } // FK เชื่อมกลับไป Header [cite: 2026-02-26]

    [MaxLength(10)]
    public string? MenuId { get; set; }

    public int? OrderQuantity { get; set; } = 1;

    // สถานะรายจานเพื่อให้พนักงานครัวกดเปลี่ยนได้ (Waiting, Cooking, Served) [cite: 2026-03-07]
    [MaxLength(20)]
    public string? ItemStatus { get; set; } = "Waiting";

    [ForeignKey("OrderHeaderId")]
    public virtual OrderHeader? OrderHeader { get; set; }

    [ForeignKey("MenuId")]
    public virtual Menu? Menu { get; set; }
}