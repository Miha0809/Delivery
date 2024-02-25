using System.ComponentModel.DataAnnotations;

namespace Delivery.Models.DTOs;

public class AuthDto
{
    [StringLength(128, MinimumLength = 5)]
    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }
    
    [StringLength(ushort.MaxValue, MinimumLength = 8)]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
}
