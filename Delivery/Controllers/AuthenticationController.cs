using Delivery.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace Delivery.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController(UserManager<User> userManager) : Controller
{
    /// <summary>
    /// Авторизація.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     {
    ///         "email": "test@gmail.com",
    ///         "password": "Test1234,",
    ///         "role": "Admin"
    ///     }
    /// 
    /// </remarks>
    /// <param name="model">Реєстрація.</param>
    /// <returns>Інформація про реєстрацію.</returns>
    /// <exception cref="Exception"></exception>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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