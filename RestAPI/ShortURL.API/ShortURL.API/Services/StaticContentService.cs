using AutoMapper;
using ShortURL.API.Domain.Models;
using ShortURL.API.DTOs.StaticContentDtos;
using ShortURL.API.Exceptions;
using ShortURL.API.Repositories;

namespace ShortURL.API.Services
{
    public class StaticContentService: IStaticContentService
    {
        private readonly IStaticContentRepository _staticContentRepository;
        private readonly IMapper _mapper;
        public StaticContentService(IStaticContentRepository staticContentRepository, IMapper mapper)
        {
            _staticContentRepository = staticContentRepository;
            _mapper = mapper;
        }
        public async Task<StaticContentDto> GetContentByPageTagAsync(string pageTag)
        {
            var content = await _staticContentRepository.GetPageByTagAsync(pageTag);

            if (content == null)
            {
                throw new EntityNotFoundException($"Content with tag {pageTag} not found");
            }

            return _mapper.Map<StaticContentDto>(content);
        }
       
        public async Task<StaticContentDto> UpdateContentAsync(string pageTage, StaticContentDto staticContentDto)
        {
            var content = await _staticContentRepository.UpdatePageByTagAsync(pageTage, _mapper.Map<StaticContent>(staticContentDto));

            if (content == null)
            {
                throw new EntityNotFoundException($"Content with tag {pageTage} not found");
            }

            return _mapper.Map<StaticContentDto>(content);
        }
    }
}
