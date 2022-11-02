using Microsoft.EntityFrameworkCore;
using Model;
using Repository.Interfaces;

namespace Repository;

public class OrderRepository : Repository<Order, Guid>, IOrderRepository
{
    public OrderRepository(DbContext context) : base(context)
    {
    }
}
