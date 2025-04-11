using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShortURL.API.DTOs.UserDtos;
using ShortURL.API.Services;

namespace ShortURL.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await _authService.LoginAsync(loginDto);
            return Ok(result);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            var result = await _authService.RegisterAsync(userRegisterDto);

            if(result.Errors != null && result.Errors.Any())
            {
                return BadRequest(result);
            }

            return Ok();
        }

        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromForm] AuthResultDto authResultDto)
        {
            bool verify = await _authService.VerifyRefreshToken(authResultDto);

            if (!verify)
            {
                return Unauthorized();
            }

            return Ok(authResultDto);
        }

        [HttpGet("user/{email}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            return Ok(await _authService.GetUserByEmailAsync(email));
        }

        [HttpGet("user/{id:int}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetUserById(int id)
        {
            return Ok(await _authService.GetUserByIdAsync(id));
        }

        
    }
}
