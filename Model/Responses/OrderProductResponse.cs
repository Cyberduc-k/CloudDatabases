namespace Model.Responses;

public class OrderProductResponse
{
    public ProductResponse Product { get; set; }
    public int Count { get; set; }

    public OrderProductResponse()
    {
    }

    public OrderProductResponse(ProductResponse product, int count)
    {
        Product = product;
        Count = count;
    }
}
