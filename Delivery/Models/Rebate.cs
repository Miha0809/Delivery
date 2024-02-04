using System.ComponentModel.DataAnnotations;

namespace Delivery.Models;

public class Rebate
{
    [Key]
    public int Id { get; set; }
    
    [Display(Name = "Нова ціна")]
    [DataType(DataType.Currency)]
    [Range(0, uint.MaxValue)]
    public uint? NewPrice { get; set; }
    
    [Display(Name = "Знижка")]
    public bool IsRebate { get; set; } = false;
}