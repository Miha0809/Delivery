namespace Delivery.Models.DTOs;

public class UserInfoFullDto
{
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required int Age { get; set; }
    public List<FavoriteDto>? Favorites { get; set; }
}