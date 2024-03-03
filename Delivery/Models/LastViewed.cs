using System.ComponentModel.DataAnnotations;

namespace Delivery.Models;

public class LastViewed
{
    [Key]
    public int Id { get; set; }
    public int ProductId { get; set; }
    
    public required string UserId { get; set; }
}