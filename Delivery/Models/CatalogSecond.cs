using System.ComponentModel.DataAnnotations;

namespace Delivery.Models;

public class CatalogSecond
{
    [Key]
    public int Id { get; set; }
    
    public required string Name { get; set; }

    public virtual List<Category>? Categories { get; set; }
}