using AutoMapper;
using Delivery.Models;
using Delivery.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProfileUserController(UserManager<ApplicationUser> userManager, IMapper mapper) : ControllerBase
{
    [HttpGet("personal_information")]
    public async Task<IActionResult> GetInformation()
    {
        var user = await userManager.GetUserAsync(User);
        return Ok(mapper.Map<UserDto>(user));
    }
}