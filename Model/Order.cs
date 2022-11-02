namespace Model;

public class Order : EntityBase
{
    public User User { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime? ShippingDate { get; set; }
    public virtual ICollection<OrderProduct> Products { get; set; }

    public Order()
    {
    }

    public Order(Guid id, User user, DateTime orderDate, DateTime? shippingDate, ICollection<OrderProduct> product)
    {
        Id = id;
        User = user;
        OrderDate = orderDate;
        ShippingDate = shippingDate;
        Products = product;
    }
}
