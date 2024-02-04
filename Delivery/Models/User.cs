using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Delivery.Models;

public class User : IdentityUser
{
    [Display(Name = "Ім'я")]
    [StringLength(16, MinimumLength = 2)]
    public string? FirstName { get; set; }
    
    [Display(Name = "Фамілія")]
    [StringLength(32, MinimumLength = 2)]
    public string? LastName { get; set; }
    
    [Display(Name = "Років")]
    public int Age { get; set; }
}