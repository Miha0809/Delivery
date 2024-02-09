
using System.ComponentModel.DataAnnotations;

namespace Delivery.Models;

public class RegisterView
{
    [StringLength(128, MinimumLength = 5)]
    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }
    
    [StringLength(ushort.MaxValue, MinimumLength = 8)]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
    
    [EnumDataType(typeof(Roles))]
    public required string Role { get; set; }
}