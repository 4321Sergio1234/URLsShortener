using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShortURL.API.DTOs.StaticContentDtos;
using ShortURL.API.Services;

namespace ShortURL.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StaticContentController : ControllerBase
    {
        private readonly IStaticContentService _staticContentService;

        public StaticContentController(IStaticContentService staticContentService)
        {
            _staticContentService = staticContentService;
        }

        [HttpGet("{pageTag}")]
        [AllowAnonymous]
        public async Task<ActionResult> Get(string pageTag)
        {
            return Ok(await _staticContentService.GetContentByPageTagAsync(pageTag));
        }

        [HttpPut("{pageTag}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Update(string pageTag, [FromBody] StaticContentDto staticContentDto)
        {
            return Ok(await _staticContentService.UpdateContentAsync(pageTag, staticContentDto));
        }
    }
}
