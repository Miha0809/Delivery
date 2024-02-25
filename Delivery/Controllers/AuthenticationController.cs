using AutoMapper;
using Delivery.Models;
using Delivery.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper) : Controller
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
    public async Task<IActionResult> Register([FromBody] AuthDto model)
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
                    await userManager.AddToRoleAsync(user, nameof(Roles.Customer));
                    user = await userManager.FindByEmailAsync(model.Email);
                    return Ok(mapper.Map<UserDto>(user));
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

    /// <summary>
    /// Авторизація.
    /// </summary>
    /// <param name="model">Аутентифікація.</param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] AuthDto model)
    {
        var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);
        
        if (result.Succeeded)
        {
            return Ok("Logged in successfully");
        }

        return Unauthorized("Invalid email or password");
    }
}