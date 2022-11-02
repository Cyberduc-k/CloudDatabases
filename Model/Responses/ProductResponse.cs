namespace Model.Responses;

public class ProductResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public ICollection<Review> Reviews { get; set; }

    public ProductResponse()
    {
    }

    public ProductResponse(Guid id, string name, string imageUrl, ICollection<Review> reviews)
    {
        Id = id;
        Name = name;
        ImageUrl = imageUrl;
        Reviews = reviews;
    }
}
