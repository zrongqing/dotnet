using ContosoPizzaSqlLite.Data;
using ContosoPizzaSqlLite.Models;

namespace ContosoPizzaSqlLite
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            // 增加
            using (var context = new ContosoPizzaDbContext())
            {
                var veggieSpecial = new Product
                {
                    Name = "Veggie Special Pizza",
                    Price = 9.99M,
                    Description = string.Empty,
                };
                context.Products.Add(veggieSpecial);

                var deluxeMeat = new Product
                {
                    Name = "Deluxe Meat Pizza",
                    Price = 12.99M,
                    Description = string.Empty
                };
                context.Add(deluxeMeat);

                // context.Products.Add(veggieSpecial);
                // context.Add(deluxeMeat);
                // 这两个是一致的，EFCore会自动推断

                context.SaveChanges();
            }
            // 查询
            using (var context = new ContosoPizzaDbContext())
            {
                var products = context.Products
                    .Where(p => p.Price > 10.00M)
                    .OrderBy(p => p.Name).ToList();

                foreach (var p in products)
                {
                    Console.WriteLine($"Id:    {p.Id}");
                    Console.WriteLine($"Name:  {p.Name}");
                    Console.WriteLine($"Price: {p.Price}");
                    Console.WriteLine(new string('-', 20));
                }
            }
        }
    }
}
