using EFCoreBasics.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreBasics.Data
{
    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlServer(@"Server = localhost; Database = BloggingDB; Trusted_Connection=True;TrustServerCertificate=True;");
            //optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=BloggingDB;Trusted_Connection=True;");
        }
    }
}
