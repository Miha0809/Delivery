using System.ComponentModel.DataAnnotations;

namespace Delivery.Models;

public class Brand
{
    [Key]
    public int Id { get; set; }
    
    [Display(Name = "Ім'я бренду")]
    [StringLength(128, MinimumLength = 2)]
    public string Name { get; set; }
}