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
public class ProductController(DeliveryDbContext context, UserManager<User> userManager, IMapper mapper) : Controller
{
    /// <summary>
    /// Продукт по id.
    /// </summary>
    /// <param name="id">Id продукта.</param>
    /// <returns>Продукт.</returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetProductById(int id)
    {
        var user = await userManager.GetUserAsync(User);
        var productById = context.Products.ToList().FirstOrDefault(product => product.Id.Equals(id));

        if (user is not null && productById is not null)
        {
            var lastViewed = new LastViewed
            {
                Product = productById,
                User = user
            };

            await context.LastViewed.AddAsync(lastViewed);
            await context.SaveChangesAsync();
        }
        
        return Ok(mapper.Map<ProductDto>(productById));
    }

    /// <summary>
    /// Всі продукти.
    /// </summary>
    /// <returns>Всі продукти із БД.</returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetProducts()
    {
        var products = await context.Products.ToListAsync();
        return Ok(mapper.Map<List<Product>, List<ProductDto>>(products));
    }
    
    /// <summary>
    /// Додавання продукта.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     {
    ///         "name": "string",
    ///         "weight": 0,
    ///         "price": 0,
    ///         "seller": {
    ///             "email": "string",
    ///             "firstName": "string",
    ///             "lastName": "string",
    ///             "age": 0
    ///         },
    ///         "images": [
    ///             {
    ///                 "link": "string"
    ///             },
    ///             {
    ///                 "link": "string"
    ///             }
    ///         ],
    ///         "catalogFirst": {
    ///             "name": "string"
    ///         },
    ///         "catalogSecond": {
    ///             "name": "string"
    ///         },
    ///         "category": {
    ///             "name": "string"
    ///         }
    ///     }
    /// 
    /// </remarks>
    /// <param name="product">Продукт.</param>
    /// <returns>Повертає замапений проудкт.</returns>
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] Product product)
    {
        var user = await userManager.GetUserAsync(User);
        product.Seller ??= user;
        
        if (ModelState.IsValid && user is not null)
        {
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();

            return Ok(mapper.Map<ProductDto>(product));
        }

        return BadRequest($"Model {nameof(product)} is not valid");
    }

    /// <summary>
    /// Видалення продукта.
    /// </summary>
    /// <param name="id">Id продукта.</param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await context.Products.FirstOrDefaultAsync(product1 => product1.Id.Equals(id));

        context.Images.RemoveRange(product.Images);
        context.Products.Remove(product);
        await context.SaveChangesAsync();

        return Ok("Product is removed");
    }

    /// <summary>
    /// Редагування продукта.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     {
    ///         "name": "string",
    ///         "weight": 0,
    ///         "price": 0,
    ///         "images": [
    ///             {
    ///                 "link": "string"
    ///             },
    ///             {
    ///                 "link": "string"
    ///             }
    ///         ],
    ///         "catalogFirst": {
    ///             "name": "string"
    ///         },
    ///         "catalogSecond": {
    ///             "name": "string"
    ///         },
    ///         "category": {
    ///             "name": "string"
    ///         }
    ///     }
    /// 
    /// </remarks>
    /// <param name="id">Id продукта.</param>
    /// <param name="productDto">Новий відредагований продукт.</param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Change(int id, [FromBody] ProductDto productDto)
    {
        productDto.Seller = mapper.Map<UserDto>(await userManager.GetUserAsync(User));
        
        var product = await context.Products.FirstOrDefaultAsync(product => product.Seller != null &&
                                                                            (product.Id.Equals(id) &&
                                                                             product.Seller.Email!.Equals(productDto.Seller.Email)));
        
        if (product is not null)
        {
            context.Products.Update(product);
            await context.SaveChangesAsync();

            return Ok(product);
        }
        
        return BadRequest("Doesn't exist product");
    }
}