using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Responses;
using Repository.Interfaces;
using Service.Exceptions;
using Service.Interfaces;

namespace Service;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    private IIncludableRepository<Product, Review> Query() => _productRepository
        .Include(p => p.Reviews);

    public async Task<ICollection<ProductResponse>> GetProducts()
    {
        return await Query().GetAll().Select(p => _mapper.Map<ProductResponse>(p)).ToArrayAsync();
    }

    public async Task<ProductResponse> GetProduct(Guid id)
    {
        Product product = await Query().GetBy(p => p.Id == id) ?? throw new NotFoundException("product");
        return _mapper.Map<ProductResponse>(product);
    }
}
