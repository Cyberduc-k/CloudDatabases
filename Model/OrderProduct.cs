namespace Model;

public class OrderProduct
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int Count { get; set; }

    public Order Order { get; set; }
    public Product Product { get; set; }

    public OrderProduct()
    {
    }

    public OrderProduct(Guid orderId, Guid productId, int count)
    {
        OrderId = orderId;
        ProductId = productId;
        Count = count;
    }
}
