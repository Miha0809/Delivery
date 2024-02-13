using AutoMapper;
using Delivery.Models;
using Delivery.Models.DTOs;
using Delivery.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InfoController(DeliveryDbContext context, UserManager<User> userManager, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Всі користувачі в БД.
    /// </summary>
    /// <returns>Всіх користувачів із БД.</returns>
    [HttpGet("/users")]
    public async Task<IActionResult> GetUsers()
    {
        var users = await context.Users.ToListAsync();
        return Ok(mapper.Map<List<UserDto>>(users));
    }

    /// <summary>
    /// Всі ролі в БД.
    /// </summary>
    /// <returns>Всі ролі із БД.</returns>
    [HttpGet("/roles")]
    public async Task<IActionResult> GetRoles()
    {
        return Ok(await context.Roles.ToListAsync());
    }

    private List<CatalogFirst> catalogFirst = new()
    {
        new()
        {
            Name = "Promotional offers",
            CatalogSeconds = null
        },
        new()
        {
            Name = "Grocery",
            CatalogSeconds = new List<CatalogSecond>()
            {
                new()
                {
                    Name = "Cereals",
                    Categories = new List<Category>()
                    {
                        new() { Name = "Bulgur" },
                        new() { Name = "Oat groats" },
                        new() { Name = "Pea" },
                        new() { Name = "Buckwheat" },
                        new() { Name = "Bean" },
                        new() { Name = "Corn grits" },
                        new() { Name = "Couscous" },
                        new() { Name = "Kutya" },
                        new() { Name = "Semolina" },
                        new() { Name = "Wheat groats" },
                        new() { Name = "Millet groats" },
                        new() { Name = "Fig" },
                        new() { Name = "Lentil" },
                        new() { Name = "Lentil" }
                    }
                },
                new()
                {
                    Name = "Flour",
                    Categories = null
                },
                new()
                {
                    Name = "Porridge and muesli",
                    Categories = null
                },
                new()
                {
                    Name = "Macaroni",
                    Categories = new List<Category>()
                    {
                        new() { Name = "Vermicelli" },
                        new() { Name = "Noodles" },
                        new() { Name = "Horns" },
                        new() { Name = "Spaghetti" },
                        new() { Name = "figurines" },
                        new() { Name = "Fusilli" }
                    }
                }
            }
        },
        new()
        {
            Name = "Vegetables and fruits",
            CatalogSeconds = new List<CatalogSecond>()
            {
                new()
                {
                    Name = "Vegetables",
                    Categories = new List<Category>()
                    {
                        new() { Name = "Cereals" },
                        new() { Name = "Flour" },
                        new() { Name = "Porridge and muesli" },
                        new() { Name = "Pasta" },
                        new() { Name = "Vegetables" },
                        new() { Name = "Fruits" },
                        new() { Name = "Dried fruits" },
                        new() { Name = "Greens" },
                        new() { Name = "Cream" },
                        new() { Name = "Butter" },
                        new() { Name = "Milk" },
                        new() { Name = "Cheese" },
                        new() { Name = "Eggs" },
                        new() { Name = "Meat" },
                        new() { Name = "Sausage and sausages" },
                        new() { Name = "Fish" },
                        new() { Name = "Seafood" }
                    }
                },
                new()
                {
                    Name = "Fruits",
                    Categories = new List<Category>()
                    {
                        new() { Name = "Pineapples" },
                        new() { Name = "Oranges" },
                        new() { Name = "Bananas" },
                        new() { Name = "Garnet" },
                        new() { Name = "Pears" },
                        new() { Name = "Mango, nectarine, avocado" },
                        new() { Name = "Kiwi" },
                        new() { Name = "Lemon" },
                        new() { Name = "Peaches" },
                        new() { Name = "Apples" },
                        new() { Name = "Berries" }
                    }
                },
                new()
                {
                    Name = "Dried fruits",
                    Categories = new List<Category>()
                    {
                        new() { Name = "Cherry" },
                        new() { Name = "Dates" },
                        new() { Name = "Dried apricots" },
                        new() { Name = "Prunes" },
                        new() { Name = "Assorted fruit" },
                        new() { Name = "Strawberries" },
                        new() { Name = "Raisins" },
                        new() { Name = "candied fruit" }
                    }
                },
                new()
                {
                    Name = "Green",
                    Categories = new List<Category>()
                    {
                        new() { Name = "Basil" },
                        new() { Name = "Cilantro" },
                        new() { Name = "Dill" },
                        new() { Name = "Mint" },
                        new() { Name = "Parsley" },
                        new() { Name = "Rosemary" },
                        new() { Name = "Salad" },
                        new() { Name = "Thyme" },
                        new() { Name = "Spinach" }
                    }
                }
            }
        },
        new()
        {
            Name = "Dairy, eggs, cheese",
            CatalogSeconds = new List<CatalogSecond>()
            {
                new()
                {
                    Name = "Cream",
                    Categories = new List<Category>()
                    {
                        new() { Name = "Pasteurized cream" },
                        new() { Name = "Portion cream" },
                        new() { Name = "Dry cream" },
                        new() { Name = "Whipped cream" }
                    }
                },
                new()
                {
                    Name = "Butter",
                    Categories = null
                },
                new()
                {
                    Name = "Milk",
                    Categories = new List<Category>()
                    {
                        new() { Name = "Lactose free" },
                        new() { Name = "Pasteurized" },
                        new() { Name = "Yarn" },
                        new() { Name = "Vegetable" },
                        new() { Name = "Dry" },
                        new() { Name = "Ultrapasteurized" }
                    }
                },
                new()
                {
                    Name = "Cheese",
                    Categories = new List<Category>()
                    {
                        new() { Name = "Smoked" },
                        new() { Name = "Cream cheese" },
                        new() { Name = "Smoked" },
                        new() { Name = "Smoked" },
                        new() { Name = "Smoked" },
                        new() { Name = "Smoked" },
                        new() { Name = "Smoked" },
                        new() { Name = "Smoked" }
                    }
                },
                new()
                {
                    Name = "Eggs",
                    Categories = null
                }
            }
        },
        new()
        {
            Name = "Meat, fish, poultry",
            CatalogSeconds = new List<CatalogSecond>()
            {
                new()
                {
                    Name = "Meat",
                    Categories = new List<Category>()
                    {
                        new() { Name = "Mutton" },
                        new() { Name = "Duck" },
                        new() { Name = "Rabbit" },
                        new() { Name = "Chicken" },
                        new() { Name = "Semi-finished products" },
                        new() { Name = "Pork" },
                        new() { Name = "Veal" },
                        new() { Name = "Stuffing" },
                        new() { Name = "Beef" }
                    }
                },
                new()
                {
                    Name = "Sausage and sausages",
                    Categories = new List<Category>()
                    {
                        new() { Name = "Sausages" },
                        new() { Name = "Anchovies" },
                        new() { Name = "Sausages" }
                    }
                },
                new()
                {
                    Name = "Fish",
                    Categories = new List<Category>()
                    {
                        new() { Name = "Crucian" },
                        new() { Name = "Carp" },
                        new() { Name = "Salmon" },
                        new() { Name = "Catfish" },
                        new() { Name = "Salmon" },
                        new() { Name = "Zander" },
                        new() { Name = "Tovtsolobyk" },
                        new() { Name = "Trout" },
                        new() { Name = "Pike" },
                        new() { Name = "Another fish" }
                    }
                },
                new()
                {
                    Name = "Seafood",
                    Categories = new List<Category>()
                    {
                        new() { Name = "Caviar" },
                        new() { Name = "Seaweed" },
                        new() { Name = "In brine" },
                        new() { Name = "Other seafood" },
                    }
                }
            }
        },
        new()
        {
            Name = "Sweets",
            CatalogSeconds = null
        },
        new()
        {
            Name = "Pastries",
            CatalogSeconds = null
        }
    };

    /// <summary>
    /// Всі категорії в БД.
    /// </summary>
    /// <returns>Всі категорії із БД.</returns>
    [HttpGet("/all_categories")]
    public async Task<IActionResult> AddCategories()
    {
        if (!context.CatalogFirst.Any())
        {
            foreach (var first in catalogFirst)
            {
                await context.CatalogFirst.AddAsync(first);

                if (first.CatalogSeconds != null)
                {
                    foreach (var second in first.CatalogSeconds)
                    {
                        await context.CatalogSecond.AddAsync(second);

                        if (second.Categories != null)
                        {
                            foreach (var category in second.Categories)
                            {
                                await context.Categories.AddAsync(category);
                            }
                        }
                    }
                }
            }
        }

        await context.SaveChangesAsync();
        
        return Ok(await context.CatalogFirst.ToListAsync());
    }

    /// <summary>
    /// Видалити всі категорії в БД.
    /// </summary>
    /// <returns>Всі категорії в БД.</returns>
    [HttpDelete("/remove_categories")]
    public async Task<IActionResult> RemoveCategories()
    {
        foreach (var first in context.CatalogFirst.Include(catalogFirst => catalogFirst.CatalogSeconds)
                     .ThenInclude(catalogSecond => catalogSecond.Categories).ToList())
        {
            if (first.CatalogSeconds != null)
            {
                foreach (var second in first.CatalogSeconds)
                {
                    if (second.Categories != null)
                    {
                        foreach (var category in second.Categories)
                        {
                            context.Categories.Remove(category);
                        }

                        context.CatalogSecond.Remove(second);
                    }
                }

                context.CatalogFirst.Remove(first);
            }
        }

        return Ok(await context.CatalogFirst.ToListAsync());
    }

    /// <summary>
    /// Всі продукти в БД.
    /// </summary>
    /// <returns>Всі продукти з БД.</returns>
    [HttpGet("/all_products")]
    public async Task<IActionResult> AddProducts()
    {
        var user = await userManager.GetUserAsync(User);

        if (!context.Products.Any())
        {
            var products = new List<Product>
            {
                new()
                {
                    Name = "Tomato",
                    Weight = 10,
                    Price = 100,
                    Images = new List<Image>()
                    {
                        new() { Link = "https://i.postimg.cc/FsY7nR4K/tomato-1.jpg" },
                        new() { Link = "https://i.postimg.cc/FsY7nR4K/tomato-2.jpg" }
                    },
                    Seller = user,
                    CatalogFirst = catalogFirst[1],
                    CatalogSecond = catalogFirst[1].CatalogSeconds![0],
                    Category = catalogFirst[1].CatalogSeconds![0].Categories![8]
                },
                new()
                {
                    Name = "Cucumber",
                    Weight = 12,
                    Price = 96,
                    Images = new List<Image>()
                    {
                        new() { Link = "https://i.postimg.cc/Mp18rk34/cucumber-1.jpg" },
                        new() { Link = "https://i.postimg.cc/Mp18rk34/cucumber-2.jpg" },
                    },
                    Seller = user,
                    CatalogFirst = catalogFirst[1],
                    CatalogSecond = catalogFirst[1].CatalogSeconds![0],
                    Category = catalogFirst[1].CatalogSeconds![0].Categories![6]
                }
            };

            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();
        }

        return Ok(await context.Products.ToListAsync());
    }

    /// <summary>
    /// Видаляє всі продукти в БД.
    /// </summary>
    /// <returns>Всі продукти із БД.</returns>
    [HttpDelete("/remove_products")]
    public async Task<IActionResult> RemoveProducts()
    {
        context.Images.RemoveRange(context.Images.ToList());
        context.Products.RemoveRange(context.Products.ToList());
        await context.SaveChangesAsync();

        return Ok(await context.Products.ToListAsync());
    }
}