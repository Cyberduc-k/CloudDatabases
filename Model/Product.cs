namespace Model;

public class Product : EntityBase
{
    public string Name { get; set; }
    public Guid ImageId { get; set; }
    public virtual ICollection<Review> Reviews { get; set; }

    public virtual ICollection<OrderProduct> Orders { get; set; }

    public Product()
    {
    }

    public Product(Guid id, string name, Guid imageId)
    {
        Id = id;
        Name = name;
        ImageId = imageId;
    }
}
