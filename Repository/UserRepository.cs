using Microsoft.EntityFrameworkCore;
using Model;
using Repository.Interfaces;

namespace Repository;

public class UserRepository : Repository<User, Guid>, IUserRepository
{
    public UserRepository(DbContext context) : base(context)
    {
    }
}
