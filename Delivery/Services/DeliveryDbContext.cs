using Delivery.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Services;

public class DeliveryDbContext : IdentityDbContext<ApplicationUser>
{
    public DeliveryDbContext(DbContextOptions<DeliveryDbContext> options) : base(options)
    {
    }
}