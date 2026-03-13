using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuffetAPI.Models;

[Table("Employee")] // กำหนดชื่อตาราง
public partial class Employee
{
    [Key] // เป็น Primary Key
    [MaxLength(10)] // กำหนดความยาวสูงสุด 10 ตัวอักษร
    public string EmployId { get; set; } = null!;

    [MaxLength(10)]
    public string? ShopId { get; set; } // Foreign Key

    [Required] // ห้ามเป็นค่าว่าง
    [MaxLength(50)]
    public string EmployName { get; set; } = null!;

    [MaxLength(20)]
    public string? EmployPosition { get; set; }

    [Column(TypeName = "date")] // ระบุประเภทใน DB เป็น date
    public DateOnly? EmploySdate { get; set; }

    // Navigation Property (ความสัมพันธ์)
    public virtual ICollection<Service> Services { get; set; } = new List<Service>();

    [ForeignKey("ShopId")] // บอกว่าตัวนี้เชื่อมกับ ShopId ด้านบน
    public virtual Shop? Shop { get; set; }

    //
    // เพิ่มฟิลด์เหล่านี้ในไฟล์ Employee.cs
    [Required]
    [MaxLength(50)]
    public string Username { get; set; } = null!;

    [Required]
    public string PasswordHash { get; set; } = null!; // สำหรับเก็บรหัสที่ผ่านการ Hash

    [Required]
    [MaxLength(20)]
    public string Role { get; set; } = null!; // เช่น "Admin", "Finance", "Warehouse"
}