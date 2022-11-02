namespace Model.DTO;

public class OrderDTO
{
    public Guid UserId { get; set; }
    public ICollection<OrderProduct> Products { get; set; }

    public OrderDTO()
    {
    }

    public OrderDTO(Guid userId, ICollection<OrderProduct> products)
    {
        UserId = userId;
        Products = products;
    }

    public class OrderProduct
    {
        public Guid ProductId { get; set; }
        public int Count { get; set; }
    }
}
