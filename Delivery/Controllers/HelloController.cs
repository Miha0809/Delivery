using Delivery.Models;
using Delivery.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
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
    
    [HttpGet("abc")]
    public async Task<IActionResult> GetAbc()
    {
        return Ok(await _context.Roles.ToListAsync());
    }
}
