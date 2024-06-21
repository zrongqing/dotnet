
using EFCoreBasics.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EFCoreBasics.Data;

[DbContext(typeof(ContosoPizzaContext))]
public class ContosoPizzaContext : DbContext
{
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<OrderDetail> OrderDetails { get; set; } = null!;

    protected override void OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(AppDbConfig.SqlServerDefault);
    }
}
    