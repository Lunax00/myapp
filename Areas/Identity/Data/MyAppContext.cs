using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyApp.Areas.Identity.Data;
using MyApp.Models;

namespace MyApp.Data;

public class MyAppContext : IdentityDbContext<MyAppUser>
{
    public MyAppContext(DbContextOptions<MyAppContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

public DbSet<MyApp.Models.Order> Order { get; set; } = default!;

public DbSet<MyApp.Models.Product> Product { get; set; } = default!;

public DbSet<MyApp.Models.Supplier> Supplier { get; set; } = default!;

public DbSet<MyApp.Models.StockMovement> StockMovement { get; set; } = default!;
}
