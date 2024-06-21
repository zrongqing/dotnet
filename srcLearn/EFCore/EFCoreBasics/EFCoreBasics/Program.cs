using EFCoreBasics.Data;
using EFCoreBasics.Models;

namespace EFCoreBasics;

internal class Program
{
    private static void Main(string[] args)
    {
        //using (var context = new BloggingContext())
        //{
        //    // 确保数据库已创建
        //    context.Database.EnsureCreated();
        //    var canConnect = context.Database.CanConnect();

        //    // 创建新博客
        //    var blog = new Blog { Url = "http://sample.com" };
        //    context.Blogs.Add(blog);
        //    context.SaveChanges();

        //    // 读取博客
        //    var blogs = context.Blogs.ToList();
        //    Console.WriteLine("All blogs in the database:");
        //    foreach (var b in blogs)
        //    {
        //        Console.WriteLine($" - {b.Url}");
        //    }

        //    // 更新博客
        //    var firstBlog = context.Blogs.First();
        //    firstBlog.Url = "http://updatedsample.com";
        //    context.SaveChanges();

        //    // 删除博客
        //    context.Blogs.Remove(firstBlog);
        //    context.SaveChanges();
        //}


        //using (var context = new AppDbContext())
        //{
        //    var isCreate = context.Database.CanConnect();

        //    var employee = new Employee
        //    {
        //        EmployeeId = 1,
        //        FirstName = "Sahana",
        //        LastName = "Bhat",
        //        Salary = 50000
        //    };

        //    context.Employees.Add(employee);
        //    context.SaveChanges();

        //    context.Employees.Remove(employee);
        //    context.SaveChanges(true);

        //    context.Employees.Update(employee);
        //    context.SaveChanges();
        //}

        //;

        //Console.WriteLine("Hello, World!");
    }
}
