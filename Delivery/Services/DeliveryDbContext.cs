using Delivery.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Services;

public class DeliveryDbContext : IdentityDbContext<User>
{
    public DbSet<Product> Products { get; set; }
    public DbSet<CatalogFirst> CatalogFirst { get; set; }
    public DbSet<CatalogSecond> CatalogSecond { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Image> Images { get; set; }
    
    public DeliveryDbContext(DbContextOptions<DeliveryDbContext> options) : base(options)
    {
    }
}