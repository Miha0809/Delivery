namespace Delivery.Models.DTOs;

public class UserPrivateInfoDto
{
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required int Age { get; set; }
    public List<Favorite>? Favorites { get; set; }
    public List<Cart>? Carts { get; set; }
    public List<LastViewedDto>? LastViewed { get; set; }
}