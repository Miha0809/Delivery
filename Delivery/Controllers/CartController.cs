using Delivery.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Delivery.Context;

namespace Delivery.Controllers;

[ApiController]
[Route("api/[controller]")]
// // [Authorize] TODO: Uncomment
public class CartController(DeliveryDbContext context, UserManager<User> userManager) : Controller
{
    /// <summary>
    /// Кошик користувача.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Carts()
    {
        var user = await userManager.GetUserAsync(User);
        return Ok(context.Users.Find(user.Id)?.Carts);
    }

    /// <summary>
    /// Зберегти товар в кошик.
    /// </summary>
    /// <param name="id">Обраний продукт.</param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost("{id}")]
    public async Task<IActionResult> Add(int id)
    {
        var user = await userManager.GetUserAsync(User);

        if (user is not null && IsExistsProduct(id))
        {
            var product = await context.Products.FindAsync(id);
            var cart = new Cart()
            {
                ProductId = product.Id
            };

            user.Carts?.Add(cart);
            context.Users.Update(user);
            await context.SaveChangesAsync();

            return Ok(context.Users.Find(user.Id)!.Carts);
        }

        return BadRequest();
    }

    private bool IsExistsProduct(int id)
    {
        return context.Products.Find(id) != null;
    }

    /// <summary>
    /// Видалити товар з кошика.
    /// </summary>
    /// <param name="id">Id кошика.</param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (context.Carts.Find(id) is not null)
        {
            var cart = await context.Carts.FindAsync(id);

            context.Carts.Remove(cart);
            await context.SaveChangesAsync();

            return Ok("Product is delete from cart");
        }

        return BadRequest();
    }
}
