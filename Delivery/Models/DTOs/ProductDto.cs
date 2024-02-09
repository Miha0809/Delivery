namespace Delivery.Models.DTOs;

public class ProductDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required double Weight { get; set; }
    public required uint Price { get; set; }

    public required UserDto Seller { get; set; }
    public required List<ImageDto> Images { get; set; }
    
    public required CatalogFirstDto CatalogFirst { get; set; }
    public required CatalogSecondDto CatalogSecond { get; set; }
    public required CategoryDto Category { get; set; }
}