using Delivery.Models;
using Delivery.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HomeController(DeliveryDbContext context, UserManager<User> userManager) : Controller
{
    
}