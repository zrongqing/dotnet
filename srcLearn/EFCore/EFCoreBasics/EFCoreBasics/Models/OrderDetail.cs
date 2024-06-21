namespace EFCoreBasics.Models;

public class OrderDetail
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public int ProductId { get; set; }
    public string OrderId { get; set; }

    public Order Order { get; set; } = null!;
    public Product Product { get; set; } = null!;
}