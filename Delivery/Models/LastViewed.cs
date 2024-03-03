using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Delivery.Models;

public class LastViewed
{
    [Key]
    public int Id { get; set; }
    public int ProductId { get; set; }
    
    public required string UserId { get; set; }
    
    [ForeignKey("ProductId")]
    public virtual Product Product { get; set; }
}