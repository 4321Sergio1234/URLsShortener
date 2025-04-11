using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShortURL.API.DTOs.Pagination;
using ShortURL.API.DTOs.ShortUrlDtos;
using ShortURL.API.Services;
using System.Security.Claims;

namespace ShortURL.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ShortUrlController : ControllerBase
    {
        private readonly IShortUrlService _shortUrlService;
        public ShortUrlController(IShortUrlService shortUrlService)
        {
            _shortUrlService = shortUrlService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetUrlPage([FromQuery] PageParams pageParams) {
            return Ok(await _shortUrlService.GetShortUrlPageAsync(pageParams));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetUrlById(int id)
        {
            return Ok(await _shortUrlService.GetShortUrlByIdAsync(id));
        }

        [HttpPost("shorten")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> ShortenUrl([FromBody] CreateShortUrlDto createShortUrlDto)
        {
            return Ok(await _shortUrlService.CreateShortUrlAsync(createShortUrlDto));
        }

        [HttpPut("update")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> UpdateUrl([FromBody] ShortUrlDto shortUrlDto)
        {
            return Ok(await _shortUrlService.UpdateShortUrlAsync(shortUrlDto));
        }

        [HttpDelete("delete")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> DeleteUrl([FromQuery] int id)
        {
            return Ok(await _shortUrlService.DeleteShortUrlAsync(id));
        }
    }
}
