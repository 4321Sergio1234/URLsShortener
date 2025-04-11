using ShortURL.API.Domain.Models;
using ShortURL.API.DTOs.Pagination;
using ShortURL.API.DTOs.ShortUrlDtos;
using ShortURL.API.DTOs.UserDtos;
using System.Linq.Expressions;
using System.Security.Claims;

namespace ShortURL.API.Services
{
    public interface IShortUrlService
    {
        Task<PageResults<ViewRestrictedShortUrlDto>> GetShortUrlPageAsync(PageParams pageParams, Expression<Func<ShortUrl, bool>>? predicate = null);
        Task<ShortUrlDto> CreateShortUrlAsync(CreateShortUrlDto createShortUrlDto);
        Task<ShortUrlDto> GetShortUrlByShortenedUrl(string alias);
        Task<UserDto> GetUserByShortUrlId(int shortUrlId );
        Task<ShortUrlDto> GetShortUrlByIdAsync(int shortUrlId);
        Task<ShortUrlDto> UpdateShortUrlAsync(ShortUrlDto shortUrlDto);
        Task<ShortUrlDto> DeleteShortUrlAsync(int shortUrlId);

        string GenerateShortenedUrl(string originalUrl);
    }
}
