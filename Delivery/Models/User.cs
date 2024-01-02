using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Delivery.Models;

public class User : IdentityUser
{
    // TODO: Adding attributes
    public int Age { get; set; }
}