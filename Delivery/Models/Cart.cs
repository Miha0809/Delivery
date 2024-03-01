using System.ComponentModel.DataAnnotations;

namespace Delivery.Models;

public class Cart
{
    [Key]
    public int Id { get; set; }
    
    public int ProductId { get; set; }
}