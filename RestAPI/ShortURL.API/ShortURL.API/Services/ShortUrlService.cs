using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ShortURL.API.Domain.Models;
using ShortURL.API.DTOs.Pagination;
using ShortURL.API.DTOs.ShortUrlDtos;
using ShortURL.API.DTOs.UserDtos;
using ShortURL.API.Exceptions;
using ShortURL.API.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;

namespace ShortURL.API.Services
{
    public class ShortUrlService : IShortUrlService
    {
        private readonly IShortUrlRepository _shortUrlRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ShortUrlService(
            IHttpContextAccessor httpContextAccessor,
            IShortUrlRepository shortUrlRepository,
            IMapper mapper,
            UserManager<User> userManager)
        {
            _shortUrlRepository = shortUrlRepository;
            _mapper = mapper;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UserDto> GetUserByShortUrlId(int shortUrlId)
        {
            var user = await _shortUrlRepository.GetUserByShortUrlId(shortUrlId);

            if (user == null)
            {
                throw new EntityNotFoundException($"User that related with short URL id {shortUrlId} not found");
            }

            return _mapper.Map<UserDto>(user);
        }

        public async Task<ShortUrlDto> CreateShortUrlAsync(CreateShortUrlDto createShortUrlDto)
        {
            string shortenedUrl = GenerateShortenedUrl(createShortUrlDto.OriginalUrl);

            var shortUrl = _mapper.Map<ShortUrl>(createShortUrlDto);
            shortUrl.ShortenedUrl = shortenedUrl;

            await _shortUrlRepository.AddAsync(shortUrl);

            return _mapper.Map<ShortUrlDto>(shortUrl);
        }

        public async Task<ShortUrlDto> UpdateShortUrlAsync(ShortUrlDto shortUrlDto)
        {
            var shortUrl = await _shortUrlRepository.UpdateAsync(_mapper.Map<ShortUrl>(shortUrlDto));

            if (shortUrl == null)
            {
                throw new EntityNotFoundException($"Short URL with id {shortUrlDto.Id} not found");
            }

            return _mapper.Map<ShortUrlDto>(shortUrl);
        }

        public async Task<ShortUrlDto> DeleteShortUrlAsync(int shortUrlId)
        {
            var shortUrl = await _shortUrlRepository.GetByIdAsync(shortUrlId);

            if (shortUrl == null)
            {
                throw new EntityNotFoundException($"Short URL with id {shortUrlId} not found");
            }

            var user = await _userManager.FindByIdAsync(shortUrl.UserId.ToString());


            if (user == null || !(await _userManager.IsInRoleAsync(user, "Admin")))
            {
                throw new UnauthorizedAccessException($"You are not authorized to delete this short URL");
            }

            shortUrl = await _shortUrlRepository.DeleteByIdAsync(shortUrlId);

            return _mapper.Map<ShortUrlDto>(shortUrl);
        }

        public async Task<PageResults<ViewRestrictedShortUrlDto>> GetShortUrlPageAsync(PageParams pageParams, Expression<Func<ShortUrl,bool>>? predicate = null)
        {
            var page = await _shortUrlRepository.GetPageAsync(pageParams, predicate);
            return new PageResults<ViewRestrictedShortUrlDto> { PageNumber = page.PageNumber,
                PageSize = page.PageSize,
                PageTotal = page.PageTotal,
                PageResult = _mapper.Map<IList<ViewRestrictedShortUrlDto>>(page.PageResult)
            };
        }

        public async Task<ShortUrlDto> GetShortUrlByShortenedUrl(string alias)
        {
            var shortUrl = await _shortUrlRepository.GetShortUrlByShortenedUrl(alias);

            if (shortUrl == null)
            {
                throw new EntityNotFoundException($"Short URL with alias {alias} not found");
            }

            return _mapper.Map<ShortUrlDto>(shortUrl);
        }

        public async Task<ShortUrlDto> GetShortUrlByIdAsync(int id)
        {
            var shortUrl = await _shortUrlRepository.GetByIdAsync(id);
            if (shortUrl == null)
            {
                throw new EntityNotFoundException($"Short URL with id {id} not found");
            }

            var maped = _mapper.Map<ShortUrlDto>(shortUrl);
            maped.User = await GetUserByShortUrlId(id);

            return maped;
        }

        public string GenerateShortenedUrl(string originalUrl)
        {
            string shortenedUrl = Guid.NewGuid().ToString();
            return shortenedUrl;
        }
    }
}
