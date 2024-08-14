using MagicCommander.Domain._Shared.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MagicCommander.Infra.Data.Database._Shared
{
	public class Repository<TEntity> where TEntity : Entity
	{
		protected readonly DbContext _dbContext;

		public Repository(DbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate)
			=> await _dbContext
				.Set<TEntity>()
				.Where(predicate)
				.SingleOrDefaultAsync();

		public async Task<TEntity?> FindAsync(int id)
			=> await _dbContext
				.Set<TEntity>()
				.Where(e => e.Id.Equals(id))
				.SingleOrDefaultAsync();

		public async Task<TEntity?> FindAsync(Guid key)
		{
			if (typeof(IHasAlternateKey).IsAssignableFrom(typeof(TEntity)) {
			return await _dbContext
				.Set<TEntity>()
				.Where(e => ((IHasAlternateKey)e).Key == key)
				.SingleOrDefaultAsync();
			}
			throw new InvalidOperationException($"Entity type {typeof(TEntity).Name} does not implement IHasAlternateKey.");
		}

		public IQueryable<TEntity> GetQueryable()
			=> _dbContext
				.Set<TEntity>()
				.AsQueryable();

		public async Task<bool> ExistsAsync(int id)
			=> await GetQueryable()
				.Where(o => o.Id == id)
				.AsNoTracking()
				.AnyAsync();

		public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
			=> await GetQueryable()
				.Where(predicate)
				.AsNoTracking()
				.AnyAsync();

		public async Task InsertAsync(TEntity entity)
			=> await _dbContext
				.Set<TEntity>()
				.AddAsync(entity);

		public async Task InsertAsync(IEnumerable<TEntity> entities)
			=> await _dbContext
				.Set<TEntity>()
				.AddRangeAsync(entities);

		public void Update(TEntity entity)
		{
			if (entity is IAuditable auditableEntity)
				auditableEntity.Audit.UpdateAudit();

			_dbContext.Attach(entity);
			_dbContext.Update(entity);
		}

		public void Update(IEnumerable<TEntity> entities)
		{
			foreach (var entity in entities)
			{
				if (entity is IAuditable auditableEntity)
					auditableEntity.Audit.UpdateAudit();
			}

			_dbContext.Set<TEntity>().UpdateRange(entities);
		}
	}
}
