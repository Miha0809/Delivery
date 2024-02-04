using AutoMapper;
using Delivery.Models;
using Delivery.Models.DTOs;
using Delivery.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Controllers;

[ApiController]
[Route("api/[controller]")]
// [Authorize(Roles = $"{nameof(Roles.Admin)},{nameof(Roles.Moderator)},{nameof(Roles.Seller)}")] TODO: uncomment
public class ProductController(DeliveryDbContext context, UserManager<User> userManager, IMapper mapper) : ControllerBase
{
    [HttpGet("/product/{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetProductById(int id)
    {
        var productById = context.Products.ToList().FirstOrDefault(product => product.Id.Equals(id))!;
        return Ok(mapper.Map<ProductDto>(productById));
    }

    [HttpGet("/products")]
    [AllowAnonymous]
    public async Task<IActionResult> GetProducts()
    {
        var products = await context.Products.ToListAsync();
        return Ok(mapper.Map<List<Product>, List<ProductDto>>(products));
    }

    [HttpPost("/add")]
    public async Task<IActionResult> Add([FromBody] Product product)
    {
        var user = await userManager.GetUserAsync(User);
        product.Seller ??= user;
        
        if (ModelState.IsValid && user is not null)
        {
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();

            return Ok(mapper.Map<ProductDto>(product)); // TODO: mapping
        }

        return BadRequest($"Model {nameof(product)} is not valid");
    }

    [HttpDelete("/delete")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await context.Products.FirstOrDefaultAsync(product1 => product1.Id.Equals(id));

        context.Images.RemoveRange(product.Images);
        context.Products.Remove(product);
        await context.SaveChangesAsync();

        return Ok("Product is removed");
    }

    [HttpPut("/change/{id}")] // TODO: які ролі зможуть редагувати продукт?
    public async Task<IActionResult> Change(int id, [FromBody] ProductDto productDto)
    {
        
        // context.Products.Update();
        return null;
    }
}