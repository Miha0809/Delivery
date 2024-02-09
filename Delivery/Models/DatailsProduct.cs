using System.ComponentModel.DataAnnotations;

namespace Delivery.Models;

public class DatailsProduct
{
    [Key]
    public int Id { get; set; }
    
    [Display(Name = "Термін зберігання")]
    [Range(0, ushort.MaxValue)]
    public ushort ShelfLife { get; set; }
    
    [Display(Name = "Стан")]
    [EnumDataType(typeof(State))]
    public string State { get; set; }
    
    public virtual Brand? Brand { get; set; }
}