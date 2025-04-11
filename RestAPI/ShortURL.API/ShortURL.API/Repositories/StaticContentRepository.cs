using Microsoft.EntityFrameworkCore;
using ShortURL.API.Domain.DbContexts;
using ShortURL.API.Domain.Models;

namespace ShortURL.API.Repositories
{
    public class StaticContentRepository : Repository<StaticContent, int>, IStaticContentRepository
    {
        private readonly ShortUrlDbContext _context;
        public StaticContentRepository(ShortUrlDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<StaticContent?> GetPageByTagAsync(string pageTag)
        {
            return await _context.StaticContents.FirstOrDefaultAsync(x => x.PageTag == pageTag);
        }

        public async Task<StaticContent?> UpdatePageByTagAsync(string pageTag, StaticContent staticContent)
        {
            var entity = await _context.StaticContents.FirstOrDefaultAsync(x => x.PageTag == pageTag);

            if (entity != null)
            {
                entity.Title = staticContent.Title;
                entity.Content = staticContent.Content;
                entity.PageTag = pageTag;
                _context.Entry<StaticContent>(entity).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }

            return entity;
        }
    }
}
