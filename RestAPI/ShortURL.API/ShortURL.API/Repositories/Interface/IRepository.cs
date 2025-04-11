using ShortURL.API.DTOs.Pagination;
using System.Linq.Expressions;

namespace ShortURL.API.Repositories
{
    public interface IRepository<TEntity, TKey> where TEntity : class
    {
        public Task<PageResults<TEntity>> GetPageAsync(PageParams pageParams, Expression<Func<TEntity, bool>>? predicate = null);
        public Task<TEntity?> GetByIdAsync(TKey id);
        public Task<TEntity> AddAsync(TEntity entity);
        public Task<TEntity?> DeleteByIdAsync(TKey id);
        public Task<TEntity?> DeleteAsync(TEntity entity);
        public Task<TEntity?> UpdateByIdAsync(TKey id, TEntity entity);
        public Task<TEntity?> UpdateAsync(TEntity entity);

    }
}
