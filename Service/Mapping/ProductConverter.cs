using AutoMapper;
using Model;
using Model.Responses;
using Service.Interfaces;

namespace Service.Mapping;

public class ProductConverter : ITypeConverter<Product, ProductResponse>
{
    private readonly IImageService _imageService;

    public ProductConverter(IImageService imageService)
    {
        _imageService = imageService;
    }

    public ProductResponse Convert(Product source, ProductResponse destination, ResolutionContext context)
    {
        string imageUrl = _imageService.GetUrl(source.ImageId.ToString());

        return new(source.Id, source.Name, imageUrl, source.Reviews);
    }
}
