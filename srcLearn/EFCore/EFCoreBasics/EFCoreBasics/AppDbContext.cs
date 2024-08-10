using EFCoreBasics.Data;
using EFCoreBasics.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreBasics;

internal class AppDbContext : DbContext
{
    private string ConnectionString { get; } = AppDbConfig.SqlServerDefault;

    public DbSet<Book> Books { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // TrustServerCertificate=True 禁用SSL验证
        string connStr = "Server=.;Database=demo1;Trusted_Connection=True;TrustServerCertificate=True;";
        optionsBuilder.UseSqlServer(connStr);

        // 查看程序所执行的SQL语句
        optionsBuilder.LogTo(Console.WriteLine);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
