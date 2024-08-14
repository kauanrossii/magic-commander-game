using System.Linq.Expressions;

namespace MagicCommander.Domain._Shared.Entities;

public interface IRepository<TEntity> where TEntity : Entity
{
    Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity?> FindAsync(int id);
    Task<TEntity?> FindAsync(Guid key);
    IQueryable<TEntity> GetQueryable();
    Task<bool> ExistsAsync(int id);
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
    Task InsertAsync(TEntity entity);
    Task InsertAsync(IEnumerable<TEntity> entities);
    void Update(TEntity entity);
    void Update(IEnumerable<TEntity> entities);
}
