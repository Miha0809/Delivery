using System.ComponentModel.DataAnnotations;

namespace Delivery.Models;

public class Favorite
{
    [Key]
    public int Id { get; set; }
    
    public int ProductId { get; set; }
}