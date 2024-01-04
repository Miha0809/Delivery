
using System.ComponentModel.DataAnnotations;

namespace Delivery.Models;

public class RegisterView
{
    // TODO: setting attributes
    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }
    
    [DataType(DataType.Password)]
    public required string Password { get; set; }
    
    [EnumDataType(typeof(Roles))]
    public required string Role { get; set; }
}