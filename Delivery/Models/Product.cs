
using System.ComponentModel.DataAnnotations;

namespace Delivery.Models;

public class Product
{
    [Key]
    public int Id { get; set; }
    
    [Display(Name = "Назва продукту")]
    [DataType(DataType.Text)]
    [StringLength(128, MinimumLength = 4)]
    public string Name { get; set; }

    [Display(Name = "Вага продукту")]
    [DataType(DataType.Currency)]
    [Range(0.001, 1000)]
    public double Weight { get; set; }
    
    [Display(Name = "Ціна")]
    [DataType(DataType.Currency)]
    [Range(0, uint.MaxValue)]
    public uint Price { get; set; }

    
    public virtual required List<Image> Images { get; set; }

    public virtual User? Seller { get; set; }
    public virtual Rebate? Rebate { get; set; }
    public virtual DatailsProduct? DatailsProduct { get; set; }
    public virtual CatalogFirst? CatalogFirst { get; set; }
    public virtual CatalogSecond? CatalogSecond { get; set; }
    public virtual Category? Category { get; set; }
}