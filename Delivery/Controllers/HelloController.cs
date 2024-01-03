using Delivery.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HelloController : ControllerBase
{
    private readonly DeliveryDbContext _context;

    public HelloController(DeliveryDbContext context)
    {
        this._context = context;
    }
    
    [HttpGet("say")]
    public async Task<IActionResult> GetHello()
    {
       
        return Ok(await _context.Users.ToListAsync());
    }
    
    [HttpGet("only_customer_role")]
    [Authorize(Roles = "Customer")]
    public async Task<IActionResult> GetAbc()
    {
        return Ok(await _context.Roles.ToListAsync());
    }
}
