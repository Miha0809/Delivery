using Delivery.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController(UserManager<User> userManager) : ControllerBase
{
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterView model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var user = new User()
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                var result = await userManager.CreateAsync(user, model.Password);
    
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, model.Role);
    
                    return Ok("User registered successfully.");
                }
    
                return BadRequest(result.Errors);
            }
    
            return BadRequest("Invalid registration data.");
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        
    }
}