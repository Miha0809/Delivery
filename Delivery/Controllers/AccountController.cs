using AutoMapper;
using Delivery.Models;
using Delivery.Models.DTOs;
using Delivery.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AccountController(UserManager<User> userManager, IMapper mapper, DeliveryDbContext context) : ControllerBase
{
    [HttpGet("/personal_information")]
    public async Task<IActionResult> GetInformation()
    {
        var user = await userManager.GetUserAsync(User);
        return Ok(mapper.Map<UserDto>(user));
    }

    // [HttpDelete("/remove")]
    // public async Task<IActionResult> Remove()
    // {
    //     var user = await userManager.GetUserAsync(User);
    //     var result = await userManager.DeleteAsync(user!);
    //     
    //     if (result.Succeeded)
    //     {
    //         await Logout();
    //         return Ok("Account is deleted");
    //     }
    //     
    //     return BadRequest("Doesn't remove user");
    // }

    [HttpDelete("/logout")]
    public async Task<IActionResult> Logout()
    {
        Response.Cookies.Delete(".AspNetCore.Identity.Application");
        return Ok();
    }
}
