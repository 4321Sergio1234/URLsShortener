using ShortURL.API.Domain.Models;
using ShortURL.API.DTOs.UserDtos;

namespace ShortURL.API.Repositories
{
    public interface IShortUrlRepository: IRepository<ShortUrl, int>
    {
        public Task<ShortUrl?> GetShortUrlByShortenedUrl(string shortenedUrl);

        public Task<User?> GetUserByShortUrlId(int shortUrlId);
        // for additional logic on demand

    }
}
