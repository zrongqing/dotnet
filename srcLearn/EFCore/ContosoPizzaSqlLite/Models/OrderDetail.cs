using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizzaSqlLite.Models;

[Index("OrderId1", Name = "IX_OrderDetails_OrderId1")]
[Index("ProductId", Name = "IX_OrderDetails_ProductId")]
public partial class OrderDetail
{
    [Key]
    public int Id { get; set; }

    public int Quantity { get; set; }

    public int ProductId { get; set; }

    public string OrderId { get; set; } = null!;

    public int OrderId1 { get; set; }

    [ForeignKey("OrderId1")]
    [InverseProperty("OrderDetails")]
    public virtual Order OrderId1Navigation { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("OrderDetails")]
    public virtual Product Product { get; set; } = null!;
}
