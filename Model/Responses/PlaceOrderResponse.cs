namespace Model.Responses;

public class PlaceOrderResponse
{
    public Guid OrderId { get; set; }

    public PlaceOrderResponse()
    {
    }

    public PlaceOrderResponse(Guid orderId)
    {
        OrderId = orderId;
    }
}
