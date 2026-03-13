using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("OrderHeader")]
public partial class OrderHeader
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OrderHeaderId { get; set; }

    [MaxLength(10)]
    public string? ServiceId { get; set; }

    [Column(TypeName = "date")]
    public DateOnly? OrderDate { get; set; }

    public TimeOnly? OrderTime { get; set; }

    [MaxLength(4)]
    public string? OrderTable { get; set; }

    // สถานะภาพรวมของทั้งใบ (เช่น "Pending", "Completed")
    [MaxLength(20)]
    public string? TotalStatus { get; set; } = "Pending";

    // เชื่อมไปยังรายการอาหารหลายๆ อย่างในใบนี้ [cite: 2026-02-26]
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}