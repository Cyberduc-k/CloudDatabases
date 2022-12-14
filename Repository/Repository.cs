using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

namespace Repository;

public abstract class Repository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class
{
    private readonly DbContext _context;
    protected readonly DbSet<TEntity> _dbset;

    protected Repository(DbContext context)
    {
        _context = context;
        _dbset = context.Set<TEntity>();
    }

    public virtual async Task<TEntity?> GetById(TId id)
    {
        return await _dbset.FindAsync(id);
    }

    public async Task<TEntity?> GetBy(Expression<Func<TEntity, bool>> filter)
    {
        return await _dbset.FirstOrDefaultAsync(filter);
    }

    public IAsyncEnumerable<TEntity> GetAll()
    {
        return _dbset.AsAsyncEnumerable();
    }

    public IAsyncEnumerable<TEntity> GetAllWhere(Expression<Func<TEntity, bool>> filter)
    {
        return _dbset.Where(filter).AsAsyncEnumerable();
    }

    public async Task<bool> Any(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbset.AnyAsync(predicate);
    }

    public async Task Insert(TEntity entity)
    {
        await _dbset.AddAsync(entity);
    }

    public void Remove(TEntity entity)
    {
        _dbset.Remove(entity);
    }

    public Task SaveChanges()
    {
        return _context.SaveChangesAsync();
    }

    public IIncludableRepository<TEntity, TProp> Include<TProp>(Expression<Func<TEntity, TProp>> property)
    {
        return new IncludableRepository<TEntity, TProp>(_dbset.Include(property));
    }

    public IIncludableRepository<TEntity, TProp> Include<TProp>(Expression<Func<TEntity, IEnumerable<TProp>>> property)
    {
        return new EnumerableIncludableRepository<TEntity, TProp>(_dbset.Include(property));
    }

    public IIncludableRepository<TEntity, TProp> Include<TProp>(Expression<Func<TEntity, ICollection<TProp>>> property)
    {
        return new EnumerableIncludableRepository<TEntity, TProp>(_dbset.Include(property));
    }
}
