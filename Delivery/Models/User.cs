using Microsoft.AspNetCore.Identity;

namespace Delivery.Models;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    public int Age { get; set; }
}