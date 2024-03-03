using AutoMapper;
using Delivery.Models;
using Delivery.Models.DTOs;
using Delivery.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HomeController(DeliveryDbContext context, UserManager<User> userManager, IMapper mapper) : Controller
{
    [HttpGet("last_viewed")]
    public async Task<IActionResult> LastViewed()
    {
        var user = await userManager.GetUserAsync(User);
        var lastViewed = await context.LastViewed.Where(lv => lv.UserId.Equals(user.Id)).ToListAsync();
        
        if (lastViewed is not null)
        {
            return Ok(mapper.Map<List<LastViewed>, List<LastViewedDto>>(lastViewed));
        }

        return BadRequest();
    }

    [HttpGet("special_offers")]
    public async Task<IActionResult> SpecialOffers()
    {
        var products = await context.Products.Where(p => (p.Rebate != null && p.Rebate.IsRebate)).ToListAsync();
        return Ok(mapper.Map<List<Product>, List<ProductDto>>(products));
    }
}