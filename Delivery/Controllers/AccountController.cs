using System.Text.Encodings.Web;
using AutoMapper;
using Delivery.Models;
using Delivery.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AccountController(UserManager<User> userManager, IMapper mapper, IEmailSender emailSender) : Controller
{
    /// <summary>
    /// Інформація про користувача.
    /// </summary>
    /// <returns>Інформацію про користувача.</returns>
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [HttpGet("personal_information")]
    public async Task<IActionResult> GetInformation()
    {
        var user = await userManager.GetUserAsync(User);
        return Ok(mapper.Map<UserPrivateInfoDto>(user));
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

    /// <summary>
    /// Видялє token із кукі.
    /// </summary>
    /// <returns>Вихід.</returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpDelete("logout")]
    public async Task<IActionResult> Logout()
    {
        Response.Cookies.Delete(".AspNetCore.Identity.Application");
        return Ok();
    }

    [HttpGet("write_data")]
    public async Task<IActionResult> WriteData()
    {
        return Ok("Hey");
    }
    
    [HttpPost("resendConfirmationEmail")]
    public async Task<IActionResult> ResendConfirmationEmail([FromBody] ResendConfirmationEmail model)
    {
        var user = await userManager.FindByEmailAsync(model.Email);
        
        if (user == null)
        {
            return BadRequest("User not found");
        }

        if (user.EmailConfirmed)
        {
            return BadRequest("Email already confirmed");
        }

        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
        var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token }, Request.Scheme);

        // Send the confirmation email
        await emailSender.SendEmailAsync(model.Email, "Confirm your email", $"Please confirm your account by clicking this link: <a href='{HtmlEncoder.Default.Encode(confirmationLink)}'>link</a>");

        return Ok("Confirmation email sent");
    }

    [HttpPost("forgotPassword")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPassword model)
    {
        var user = await userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            return BadRequest("User not found");
        }

        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        var resetLink = Url.Action("ResetPassword", "Account", new { userId = user.Id, token }, Request.Scheme);

        // Send the password reset email
        await emailSender.SendEmailAsync(model.Email, "Reset your password", $"Please reset your password by clicking here: <a href='{HtmlEncoder.Default.Encode(resetLink)}'>link</a>");

        return Ok("Password reset email sent");
    }
    
    [HttpPost("confirmEmail")]
    public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmail model)
    {
        var user = await userManager.FindByIdAsync(model.UserId);
        
        if (user == null)
        {
            return BadRequest("User not found");
        }

        var result = await userManager.ConfirmEmailAsync(user, model.Token);
        
        if (!result.Succeeded)
        {
            return BadRequest("Failed to confirm email");
        }

        return Ok("Email confirmed successfully");
    }
    
    [HttpPost("resetPassword")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPassword model)
    {
        var user = await userManager.FindByIdAsync(model.UserId);
        if (user == null)
        {
            return BadRequest("User not found");
        }

        var result = await userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
        if (!result.Succeeded)
        {
            return BadRequest("Failed to reset password");
        }

        return Ok("Password reset successfully");
    }
}
