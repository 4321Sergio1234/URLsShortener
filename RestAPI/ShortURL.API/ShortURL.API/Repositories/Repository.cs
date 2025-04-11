using Microsoft.EntityFrameworkCore;
using ShortURL.API.DTOs.Pagination;
using System.Linq.Expressions;

namespace ShortURL.API.Repositories
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {
        private readonly DbContext _shortUrlDbContext;

        public Repository(DbContext shortUrlDbContext) {
            _shortUrlDbContext = shortUrlDbContext;
        }

        public virtual async Task<PageResults<TEntity>> GetPageAsync(PageParams pageParams, Expression<Func<TEntity, bool>>? predicate = null)
        {
            var query = _shortUrlDbContext.Set<TEntity>().AsQueryable();

            var totalPages = (int)Math.Ceiling((double)(await query.CountAsync() / pageParams.PageSize));

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            var pageItems = await query
                .Skip((pageParams.PageNumber - 1) * pageParams.PageSize)
                .Take(pageParams.PageSize)
                .ToListAsync();

            return new PageResults<TEntity>
            {
                PageResult = pageItems,
                PageTotal  = totalPages,
                PageNumber = pageParams.PageNumber,
                PageSize   = pageParams.PageSize
            };
        }

        public virtual async Task<TEntity?> GetByIdAsync(TKey id)
        {
            return await _shortUrlDbContext.FindAsync<TEntity>(id);
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            await _shortUrlDbContext.Set<TEntity>().AddAsync(entity);
            await _shortUrlDbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<TEntity?> DeleteByIdAsync(TKey id)
        {
            var entity = await _shortUrlDbContext.FindAsync<TEntity>(id);

            if (entity != null)
            {
                _shortUrlDbContext.Remove(entity);
                await _shortUrlDbContext.SaveChangesAsync();
                return entity;
            }
            return null;
        }

        public virtual async Task<TEntity?> DeleteAsync(TEntity entity)
        {
            var entityForDel = await _shortUrlDbContext.FindAsync<TEntity>(entity);

            if (entityForDel != null)
            {
                _shortUrlDbContext.Remove(entityForDel);
                await _shortUrlDbContext.SaveChangesAsync();
                return entityForDel;
            }

            return null;
        }

        public virtual async Task<TEntity?> UpdateByIdAsync(TKey key, TEntity entity)
        {
            var entityForUpd = await _shortUrlDbContext.FindAsync<TEntity>(key);

            if (entityForUpd != null)
            {
                _shortUrlDbContext.Entry(entityForUpd).CurrentValues.SetValues(entity);
                await _shortUrlDbContext.SaveChangesAsync();
                return entityForUpd;
            }

            return null;
        }

        public virtual async Task<TEntity?> UpdateAsync(TEntity entity)
        {
            var keyValue = _shortUrlDbContext.Entry(entity).Property("Id").CurrentValue;
            var entityForUpd = await _shortUrlDbContext.FindAsync<TEntity>(keyValue);

            if (entityForUpd != null) {
                _shortUrlDbContext.Entry(entityForUpd).CurrentValues.SetValues(entity);
                await _shortUrlDbContext.SaveChangesAsync();
                return entity;
            }

            return null;
        }
    }
}
