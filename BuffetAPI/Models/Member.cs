using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuffetAPI.Models;

[Table("Members")]
public partial class Member
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // รันเลข ID อัตโนมัติ (1, 2, 3...)
    public int MemberId { get; set; }

    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; } = null!;

    [MaxLength(100)]
    public string? LastName { get; set; }

    [Required]
    [MaxLength(20)]
    public string Phone { get; set; } = null!;

    [MaxLength(20)]
    public string? Gender { get; set; }

    [Column(TypeName = "date")]
    public DateOnly? BirthDate { get; set; }

    [MaxLength(50)]
    public string? Province { get; set; }

    public DateTime? RegisterDate { get; set; } = DateTime.Now; // กำหนดค่าเริ่มต้นเป็นเวลาปัจจุบัน

    [MaxLength(50)]
    public string? LineUserId { get; set; } // ✅ เพิ่มบรรทัดนี้ (อนุญาตให้ว่างได้ในช่วงแรก)
}