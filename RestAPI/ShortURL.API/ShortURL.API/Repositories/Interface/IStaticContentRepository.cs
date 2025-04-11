using ShortURL.API.Domain.Models;

namespace ShortURL.API.Repositories
{
    public interface IStaticContentRepository : IRepository<StaticContent, int>
    {
        public Task<StaticContent?> GetPageByTagAsync(string pageTag);
        public Task<StaticContent?> UpdatePageByTagAsync(string pageTag, StaticContent staticContent);

        // for additional logic on demand
    }
}
