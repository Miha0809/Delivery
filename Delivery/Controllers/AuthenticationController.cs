using System.Security.Claims;
using System.Text;
using Delivery.Models;
using Delivery.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Hash = Delivery.Services.Hash;

namespace Delivery.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController(DeliveryDbContext context) : ControllerBase
{
    private readonly DeliveryDbContext _context = context;

    // [HttpPost("register")]
    // public async Task<IActionResult> Register([FromBody] User user)
    // {
    //     if (!ModelState.IsValid)
    //     {
    //         return BadRequest();
    //     }
    //
    //     user.Password = Hash.GetHash(user.Password!);
    //
    //     try
    //     {
    //         // TODO: Remove ifs
    //         if (user.Role!.Name.Equals((await _context.Roles.FirstOrDefaultAsync(role => role.Name.Equals("Client")))!
    //                 .Name))
    //         {
    //             user.Role = await _context.Roles.FirstOrDefaultAsync(role => role.Name.Equals("Client"));
    //             var client = new global::Client.Models.Client()
    //             {
    //                 Name = user.Name,
    //                 Email = user.Email!,
    //                 Password = user.Password,
    //                 Role = user.Role!
    //             };
    //
    //             _context.Clients.Add(client);
    //             await _context.SaveChangesAsync();
    //         }
    //         else if (user.Role.Name.Equals(
    //                      (await _context.Roles.FirstOrDefaultAsync(role => role.Name.Equals("Company")))!.Name))
    //         {
    //             user.Role = await _context.Roles.FirstOrDefaultAsync(role => role.Name.Equals("Company"));
    //             var company = new global::Company.Models.Company()
    //             {
    //                 Name = user.Name!,
    //                 Email = user.Email!,
    //                 Password = user.Password,
    //                 Role = user.Role!
    //             };
    //
    //             _context.Companies.Add(company);
    //             await _context.SaveChangesAsync();
    //         }
    //         else if (user.Role.Name.Equals(
    //                      (await _context.Roles.FirstOrDefaultAsync(role => role.Name.Equals("Courier")))!.Name))
    //         {
    //             user.Role = await _context.Roles.FirstOrDefaultAsync(role => role.Name.Equals("Courier"));
    //
    //             var courier = new global::Courier.Models.Courier
    //             {
    //                 Name = user.Name!,
    //                 Email = user.Email!,
    //                 Password = user.Password,
    //                 Role = user.Role!,
    //             };
    //
    //             _context.Couriers.Add(courier);
    //             await _context.SaveChangesAsync();
    //         }
    //     }
    //     catch (Exception exception)
    //     {
    //         return BadRequest(exception.Message);
    //     }
    //
    //     return Ok(user);
    // }
    //
    // [HttpPost("login")]
    // public async Task<IActionResult> Login([FromBody] Login login)
    // {
    //     User? user = null;
    //
    //     user ??= await _context.Clients.FirstOrDefaultAsync(client =>
    //         client.Email.Name.Equals(login.Email) && Hash.GetHash(login.Password).Equals(client.Password));
    //
    //     user ??= await _context.Clients.FirstOrDefaultAsync(client =>
    //         client.Email.Name.Equals(login.Email) && Hash.GetHash(login.Password).Equals(client.Password));
    //
    //     user ??= await _context.Companies.FirstOrDefaultAsync(company =>
    //         company.Email.Name.Equals(login.Email) && Hash.GetHash(login.Password).Equals(company.Password));
    //
    //     user ??= await _context.Couriers.FirstOrDefaultAsync(client =>
    //         client.Email.Name.Equals(login.Email) && Hash.GetHash(login.Password).Equals(client.Password));
    //
    //     user ??= await _context.Glovos.FirstOrDefaultAsync(client =>
    //         client.Email.Name.Equals(login.Email) && Hash.GetHash(login.Password).Equals(client.Password));
    //
    //     user ??= await _context.Moderators.FirstOrDefaultAsync(client =>
    //         client.Email.Name.Equals(login.Email) && Hash.GetHash(login.Password).Equals(client.Password));
    //
    //     if (user == null || !Hash.VerifyHash(login.Password, Hash.GetHash(login.Password)) || !ModelState.IsValid)
    //     {
    //         return BadRequest();
    //     }
    //
    //     var accessToken = GenerateAccessToken(user);
    //     var refreshToken = GenerateRefreshToken();
    //     var refreshTokenDto = new RefreshToken()
    //     {
    //         Token = refreshToken,
    //         UserId = user.Id
    //     };
    //
    //     await _context.RefreshTokens.AddAsync(refreshTokenDto);
    //     await _context.SaveChangesAsync();
    //
    //     return Ok(new
    //     {
    //         access_token = accessToken,
    //         refresh_token = refreshToken
    //     });
    // }
    //
    // [Authorize]
    // [HttpDelete("logout")]
    // public async Task<IActionResult> Logout()
    // {
    //     int.TryParse(HttpContext.User.FindFirstValue("Id"), out var userId);
    //     var refresh = await _context.RefreshTokens.FirstOrDefaultAsync(token => token.UserId.Equals(userId));
    //
    //     if (refresh == null)
    //     {
    //         return BadRequest();
    //     }
    //
    //     _context.RefreshTokens.Remove(refresh);
    //     await _context.SaveChangesAsync();
    //
    //     return Ok();
    // }
    //
    // [HttpPost("refresh")]
    // public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest refresh)
    // {
    //     if (!ModelState.IsValid)
    //     {
    //         return BadRequest();
    //     }
    //
    //     var isValid = Validate(refresh.RefreshToken);
    //     var refreshToken =
    //         await _context.RefreshTokens.FirstOrDefaultAsync(token => token.Token.Equals(refresh.RefreshToken));
    //
    //     IUser? user = null;
    //     user ??= await _context.Clients.FindAsync(refreshToken!.UserId);
    //     user ??= await _context.Companies.FindAsync(refreshToken!.UserId);
    //     user ??= await _context.Couriers.FindAsync(refreshToken!.UserId);
    //     user ??= await _context.Glovos.FindAsync(refreshToken!.UserId);
    //     user ??= await _context.Moderators.FindAsync(refreshToken!.UserId);
    //
    //     if (!isValid || refreshToken == null || user == null)
    //     {
    //         return BadRequest();
    //     }
    //
    //     _context.RefreshTokens.Remove(refreshToken);
    //     await _context.SaveChangesAsync();
    //
    //     var accessToken = GenerateAccessToken(user);
    //     var refreshToken2 = GenerateRefreshToken();
    //     var refreshTokenDto = new RefreshToken()
    //     {
    //         Token = refreshToken2,
    //         UserId = user.Id
    //     };
    //
    //     await _context.RefreshTokens.AddAsync(refreshTokenDto);
    //     await _context.SaveChangesAsync();
    //
    //     return Ok(new
    //     {
    //         access_token = accessToken,
    //         refresh_token = refreshToken.Token
    //     });
    // }
    //
    // private string GenerateAccessToken(IUser user)
    // {
    //     var key = new SymmetricSecurityKey(
    //         Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("ACCESS_SECRET_KEY") ?? string.Empty));
    //     var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
    //     var claims = new List<Claim>
    //     {
    //         new("Id", user.Id.ToString()),
    //         new("Email", user.Email.Name),
    //         new("Role", user.Role.Name)
    //     };
    //     var issuer = Environment.GetEnvironmentVariable("ISSUER") ?? string.Empty;
    //     var audience = Environment.GetEnvironmentVariable("AUDIENCE") ?? string.Empty;
    //     int.TryParse(Environment.GetEnvironmentVariable("ACCESS_TIME_LIFE_KAY_MIN") ?? string.Empty, out var minutes);
    //     var token = new JwtSecurityToken(
    //         issuer: issuer,
    //         audience: audience,
    //         claims: claims,
    //         notBefore: DateTime.UtcNow,
    //         expires: DateTime.Now.AddDays(minutes * 256),
    //         signingCredentials: credentials);
    //     return new JwtSecurityTokenHandler().WriteToken(token);
    // }
    //
    // private string GenerateRefreshToken()
    // {
    //     var key = new SymmetricSecurityKey(
    //         Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("REFRESH_SECRET_KEY") ?? string.Empty));
    //     var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
    //     var issuer = Environment.GetEnvironmentVariable("ISSUER") ?? string.Empty;
    //     var audience = Environment.GetEnvironmentVariable("AUDIENCE") ?? string.Empty;
    //     int.TryParse(Environment.GetEnvironmentVariable("REFRESH_TIME_LIFE_KAY_MIN") ?? string.Empty, out var minutes);
    //     var token = new JwtSecurityToken(
    //         issuer: issuer,
    //         audience: audience,
    //         notBefore: DateTime.UtcNow,
    //         expires: DateTime.Now.AddDays(minutes * 256),
    //         signingCredentials: credentials);
    //     return new JwtSecurityTokenHandler().WriteToken(token);
    // }
    //
    // private bool Validate(string refresh)
    // {
    //     var tokenValidationParameters = new TokenValidationParameters()
    //     {
    //         IssuerSigningKey =
    //             new SymmetricSecurityKey(
    //                 Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("REFRESH_SECRET_KEY") ?? string.Empty)),
    //         ValidIssuer = Environment.GetEnvironmentVariable("ISSUER"),
    //         ValidAudience = Environment.GetEnvironmentVariable("AUDIENCE"),
    //         ValidateIssuerSigningKey = true,
    //         ValidateIssuer = true,
    //         ValidateAudience = true,
    //         ClockSkew = TimeSpan.Zero
    //     };
    //     var token = new JwtSecurityTokenHandler();
    //
    //     try
    //     {
    //         token.ValidateToken(refresh, tokenValidationParameters, out _);
    //         return true;
    //     }
    //     catch
    //     {
    //         return false;
    //     }
    // }
}