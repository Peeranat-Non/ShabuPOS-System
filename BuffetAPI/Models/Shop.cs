using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuffetAPI.Models;

[Table("Shop")]
public partial class Shop
{
    [Key]
    [MaxLength(10)]
    public string ShopId { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string ShopName { get; set; } = null!;

    [MaxLength(255)]
    public string? ShopAddress { get; set; }

    [MaxLength(10)]
    public string? ShopPhone { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}