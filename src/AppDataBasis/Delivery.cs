namespace AppDataBasis;

internal class Delivery
{
    public long Id { get; set; }
    /// <summary>
    /// 快递公司名
    /// </summary>
    public string CompanyName { get; set; }
    /// <summary>
    /// 快递单号
    /// </summary>
    public string Number { get; set; }
    /// <summary>
    /// 订单
    /// </summary>
    public Order Order { get; set; }
    /// <summary>
    /// 指向订单的外键
    /// </summary>
    public long OrderId { get; set; }      
}
