using System.ComponentModel.DataAnnotations;

namespace Delivery.Models;

public class LastViewed
{
    [Key]
    public int Id { get; set; }

    public virtual Product Product { get; set; }
    public virtual User User { get; set; }
}