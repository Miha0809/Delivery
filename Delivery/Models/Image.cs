using System.ComponentModel.DataAnnotations;

namespace Delivery.Models;

public class Image
{
    [Key]
    public int Id { get; set; }
    
    [StringLength(int.MaxValue, MinimumLength = 4)]
    public required string Link { get; set; }
}