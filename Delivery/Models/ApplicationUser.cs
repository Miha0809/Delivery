using Microsoft.AspNetCore.Identity;

namespace Delivery.Models;

public class ApplicationUser : IdentityUser
{
    public int Age { get; set; }
}