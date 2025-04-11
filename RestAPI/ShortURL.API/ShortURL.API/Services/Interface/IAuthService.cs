using ShortURL.API.Domain.Models;
using ShortURL.API.DTOs.UserDtos;

namespace ShortURL.API.Services
{
    public interface IAuthService
    {
        Task<AuthResultDto>         LoginAsync(LoginDto loginDto);
        Task<UserRegisterResultDto> RegisterAsync(UserRegisterDto userDto);
        Task<UserDto>               GetUserByEmailAsync(string email);
        Task<string>                GenerateJwtTokenAsync(User user);
        Task<UserDto>               GetUserByIdAsync(int userId);
        Task<string>                CreateRefreshToken(User user);
        Task<bool>                  VerifyRefreshToken(AuthResultDto authResultDto);
    }
}
