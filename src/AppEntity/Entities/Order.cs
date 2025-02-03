using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppEntity;

public class Order:BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid OrderId { get; set; }
    
    /// <summary>
    /// 商品名
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// 收货地址
    /// </summary>
    public string Address { get; set; }
    
    /// <summary>
    /// 快递信息
    /// </summary>
    public Delivery? Delivery { get; set; }
}