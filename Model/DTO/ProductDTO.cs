namespace Model.DTO;

public class ProductDTO
{
    public string Name { get; set; }
    public Guid ImageId { get; set; }

    public ProductDTO()
    {
    }

    public ProductDTO(string name, Guid imageId)
    {
        Name = name;
        ImageId = imageId;
    }
}
