using AutoMapper;
using Delivery.Models;
using Delivery.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController(UserManager<ApplicationUser> userManager, IMapper mapper) : ControllerBase
{
    [HttpGet("personal_information")]
    public async Task<IActionResult> GetInformation()
    {
        var user = await userManager.GetUserAsync(User);
        return Ok(mapper.Map<UserDto>(user));
    }

    [HttpDelete("remove")]
    public async Task<IActionResult> Remove()
    {
        // return Ok(await userManager.Remove);
        return BadRequest("Doesn't remove user");
    }
}
