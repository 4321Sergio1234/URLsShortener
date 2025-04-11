using Microsoft.EntityFrameworkCore;
using ShortURL.API.Domain.DbContexts;
using ShortURL.API.Domain.Models;
using ShortURL.API.DTOs.UserDtos;

namespace ShortURL.API.Repositories
{
    public class ShortUrlRepository: Repository<ShortUrl, int>, IShortUrlRepository
    {
        private readonly ShortUrlDbContext _context;
        public ShortUrlRepository(ShortUrlDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ShortUrl?> GetShortUrlByShortenedUrl(string shortenedUrl)
        {
            return await _context.ShortUrls.FirstOrDefaultAsync(x => x.ShortenedUrl == shortenedUrl);
        }

        public async Task<User?> GetUserByShortUrlId(int shortUrlId)
        {
            var shortUrl = await _context.ShortUrls.FirstOrDefaultAsync(s => s.Id == shortUrlId);
            
            if(shortUrl == null)
            {
                return null;
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == shortUrl.UserId);

            if (user == null)
            {
                return null;
            }

            return user;
        }
    }
}
