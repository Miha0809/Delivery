using Microsoft.AspNetCore.Identity;

namespace Delivery.Models;

public class Role : IdentityRole
{
    public string Name { get; set; }
}