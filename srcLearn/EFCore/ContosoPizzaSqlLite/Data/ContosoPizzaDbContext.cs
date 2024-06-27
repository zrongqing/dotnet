using ContosoPizzaSqlLite.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizzaSqlLite.Data;

public partial class ContosoPizzaDbContext : DbContext
{
    public ContosoPizzaDbContext()
    {
    }

    public ContosoPizzaDbContext(DbContextOptions<ContosoPizzaDbContext> options)
        : base(options)
    {
    }


    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=ContosoPizza.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
