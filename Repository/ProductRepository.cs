using Microsoft.EntityFrameworkCore;
using Model;
using Repository.Interfaces;

namespace Repository;

public class ProductRepository : Repository<Product, Guid>, IProductRepository
{
    public ProductRepository(DbContext context) : base(context)
    {
    }
}
