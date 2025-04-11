using ShortURL.API.DTOs.StaticContentDtos;

namespace ShortURL.API.Services
{
    public interface IStaticContentService
    {
        Task<StaticContentDto> GetContentByPageTagAsync(string pageTag);
        Task<StaticContentDto> UpdateContentAsync(string pageTage, StaticContentDto staticContentDto);
    }
}
