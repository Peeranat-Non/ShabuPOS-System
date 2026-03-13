using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuffetAPI.Models;

[Table("Service")]
public partial class Service
{
    [Key]
    [MaxLength(10)]
    public string ServiceId { get; set; } = null!;

    [MaxLength(10)]
    public string? EmployId { get; set; } // FK

    [Column(TypeName = "date")]
    public DateOnly? ServiceDate { get; set; }

    public TimeOnly? ServiceTime { get; set; }

    public int? ServiceNumberPeople { get; set; }

    [ForeignKey("EmployId")]
    public virtual Employee? Employ { get; set; }

    public virtual ICollection<OrderHeader> Orders { get; set; } = new List<OrderHeader>();

    // ✅ เพิ่ม 2 บรรทัดนี้เข้าไปครับ
    // ✅ 1. แก้เป็น PackageId (ไม่มีขีด)
    public string? PackageId { get; set; }

    // ✅ 2. แก้ ForeignKey ให้ชี้ไปที่ตัวแปรข้างบน
    [ForeignKey("PackageId")]
    public virtual Package? Package { get; set; }
}