using Model.Responses;

namespace Service.Interfaces;

public interface IProductService
{
    public Task<ICollection<ProductResponse>> GetProducts();
    public Task<ProductResponse> GetProduct(Guid id);
}
