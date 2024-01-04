using Delivery.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HomeController(UserManager<ApplicationUser> userManager) : ControllerBase
{
    
}