namespace Model.DTO;

public class PlaceOrderDTO
{
    public Guid OrderId { get; set; }
    public OrderDTO Order { get; set; }

    public PlaceOrderDTO()
    {
    }

    public PlaceOrderDTO(Guid orderId, OrderDTO order)
    {
        OrderId = orderId;
        Order = order;
    }
}
