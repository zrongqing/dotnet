using EFCoreBasics.Data;
using EFCoreBasics.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreBasics;

internal class Program
{
    private static async Task Main(string[] args)
    {
        // await AddData();
        //TestQueryable();
        ComplexityQueryBooks("爱", true, true, 30);
    }

    private static async Task AddData()
    {
        await using AppDbContext ctx = new AppDbContext();
        var b1 = new Book
        {
            AuthorName = "杨中科",
            Title = "零基础趣学C语言",
            Price = 59.8,
            PubTime = new DateTime(2019, 3, 1)
        };
        var b2 = new Book
        {
            AuthorName = "Robert Sedgewick",
            Title = "算法(第4版)",
            Price = 99,
            PubTime = new DateTime(2012, 10, 1)
        };
        var b3 = new Book
        {
            AuthorName = "吴军",
            Title = "数学之美",
            Price = 69,
            PubTime = new DateTime(2020, 5, 1)
        };
        var b4 = new Book
        {
            AuthorName = "杨中科",
            Title = "程序员的SQL金典",
            Price = 52,
            PubTime = new DateTime(2008, 9, 1)
        };
        var b5 = new Book
        {
            AuthorName = "吴军",
            Title = "文明之光",
            Price = 246,
            PubTime = new DateTime(2017, 3, 1)
        };
        ctx.Books.Add(b1);
        ctx.Books.Add(b2);
        ctx.Books.Add(b3);
        ctx.Books.Add(b4);
        ctx.Books.Add(b5);
        await ctx.SaveChangesAsync();
    }

    /// <summary>
    /// IQueryable查询测试
    /// </summary>
    /// <inheritdoc>
    /// IQueryable是延迟加载，如果不使用不会进行记载
    /// </inheritdoc>
    private static void TestQueryable()
    {
        using AppDbContext ctx = new AppDbContext();
        IQueryable<Book> books = ctx.Books.Where(b => b.Price > 1.1);
        foreach (var b in books.Where(b => b.Price > 1.1))
        {
            Console.WriteLine($"Id={b.Id},Title={b.Title}");
        }

        // 会被翻译成如下的SQL语句
        //SELECT[t].[Id], [t].[AuthorName], [t].[Price], [t].[PubTime], [t].[Title]
        //FROM[T_Books] AS[t]
        //WHERE[t].[Price] > 1.1000000000000001E0 AND[t].[Price] > 1.1000000000000001E0
    }

    /// <summary>
    /// 复杂的Queryable使用
    /// </summary>
    /// <inheritdoc>
    /// 拼接过程中不会执行SQL语句
    /// </inheritdoc>
    private static void ComplexityQueryBooks(string searchWords, bool searchAll, bool orderByPrice, double upperPrice)
    {
        using AppDbContext ctx = new AppDbContext();
        IQueryable<Book> books = ctx.Books.Where(b => b.Price <= upperPrice);
        if (searchAll) //匹配书名或作者名
        {
            books = books.Where(b => b.Title.Contains(searchWords) ||
                                     b.AuthorName.Contains(searchWords));
        }
        else //只匹配书名
        {
            books = books.Where(b => b.Title.Contains(searchWords));
        }

        if (orderByPrice) //按照价格排序
        {
        }

        foreach (Book b in books)
        {
            Console.WriteLine($"{b.Id},{b.Title},{b.Price},{b.AuthorName}");
        }
    }
}
