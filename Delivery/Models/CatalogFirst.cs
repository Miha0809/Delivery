using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Delivery.Models;

public class CatalogFirst
{
    [Key]
    public int Id { get; set; }
    
    public required string Name { get; set; }
    
    public virtual required List<CatalogSecond>? CatalogSeconds { get; set; }
}