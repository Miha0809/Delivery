namespace Delivery.Models.DTOs;

public class UserDto
{
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required int Age { get; set; }
}