namespace Model.Responses;

public class OrderResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime? ShippingDate { get; set; }
    public ICollection<OrderProductResponse> Products { get; set; }

    public OrderResponse()
    {
    }

    public OrderResponse(Guid id, Guid userId, DateTime orderDate, DateTime? shippingDate, ICollection<OrderProductResponse> products)
    {
        Id = id;
        UserId = userId;
        OrderDate = orderDate;
        ShippingDate = shippingDate;
        Products = products;
    }
}
