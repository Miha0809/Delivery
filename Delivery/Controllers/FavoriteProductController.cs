using AutoMapper;
using Delivery.Context;
using Delivery.Models;
using Delivery.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Controllers;

[ApiController]
[Route("api/[controller]")]
// [Authorize] TODO: Uncomment
public class FavoriteProductController(
    UserManager<User> userManager,
    DeliveryDbContext context,
    IMapper mapper
) : Controller
{
    /// <summary>
    /// Улюблені товари користувача.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Favorites()
    {
        var user = await userManager.GetUserAsync(User);
        // var favorites = await context.Favorites.Where(p => p.User.Id.Equals(user.Id)).ToListAsync();
        // return Ok(mapper.Map<List<FavoriteDto>>(favorites));
        return Ok(context.Users.FirstAsync(u => u.Id.Equals(user!.Id)).Result.Favorites);
    }

    /// <summary>
    /// Вибрати в обрані продукт.
    /// </summary>
    /// <param name="id">Обраний продукт.</param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost("{id}")]
    public async Task<IActionResult> Favorite(int id)
    {
        var user = await userManager.GetUserAsync(User);

        if (user is not null && IsExistsProduct(id))
        {
            var product = await context.Products.FindAsync(id);
            var favorite = new Favorite() { ProductId = product.Id };

            user.Favorites?.Add(favorite);

            // if (context.Favorites.FirstOrDefault(f => f.Product.Id.Equals(id)) != null)
            // {
            //     return BadRequest("The product is a favorite.");
            // }

            context.Users.Update(user);
            // await context.Favorites.AddAsync(favorite);
            await context.SaveChangesAsync();

            return Ok(mapper.Map<UserDto>(user));
        }

        return BadRequest();
    }

    private bool IsExistsProduct(int id)
    {
        return context.Products.Find(id) != null;
    }

    /// <summary>
    /// Видалити товар з улюблених.
    /// </summary>
    /// <param name="id">Id улюбленого продукта.</param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Disliked(int id)
    {
        if (context.Favorites.Find(id) is not null)
        {
            var favoriteProduct = await context.Favorites.FindAsync(id);

            context.Favorites.Remove(favoriteProduct);
            await context.SaveChangesAsync();

            return Ok("Product is disliked");
        }

        return BadRequest();
    }
}
