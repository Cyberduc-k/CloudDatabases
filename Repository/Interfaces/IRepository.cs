using System.Linq.Expressions;

namespace Repository.Interfaces;

public interface IRepository<TEntity, TId> where TEntity : class
{
    public Task<TEntity?> GetById(TId id);
    public Task<TEntity?> GetBy(Expression<Func<TEntity, bool>> filter);
    public IAsyncEnumerable<TEntity> GetAll();
    public IAsyncEnumerable<TEntity> GetAllWhere(Expression<Func<TEntity, bool>> filter);
    public Task<bool> Any(Expression<Func<TEntity, bool>> predicate);

    public Task Insert(TEntity entity);
    public void Remove(TEntity entity);
    public Task SaveChanges();

    public IIncludableRepository<TEntity, TProp> Include<TProp>(Expression<Func<TEntity, TProp>> property);
    public IIncludableRepository<TEntity, TProp> Include<TProp>(Expression<Func<TEntity, IEnumerable<TProp>>> property);
    public IIncludableRepository<TEntity, TProp> Include<TProp>(Expression<Func<TEntity, ICollection<TProp>>> property);
}
