using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ShortURL.API.Domain.Models;
using ShortURL.API.DTOs.UserDtos;
using ShortURL.API.Exceptions;
using ShortURL.API.Exceptions.Configurations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShortURL.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        private readonly string _loginProvider = "ShortUrlAPI";
        private readonly string _tokenName = "RefreshToken";

        public AuthService(
            IMapper mapper, 
            UserManager<User> userManager, 
            IConfiguration configuration
            )
        {
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<string> GenerateJwtTokenAsync(User user)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)
                );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var roles      = await _userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();
            var userClaims     = await _userManager.GetClaimsAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!,
                          "UserId", user.Id.ToString())
            }.Union(userClaims).Union(roleClaims);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["Jwt:DurationInMinutes"]!)),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<UserDto> GetUserByIdAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync($"{userId}");

            if (user == null)
            {
                throw new UserNotFoundException($"User with id {userId} not found");
            }

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if(user == null)
            {
                throw new UserNotFoundException($"User {email} not found");
            }

            return _mapper.Map<UserDto>(user);
        }

        public async Task<AuthResultDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, loginDto.Password)) {
                var token = await GenerateJwtTokenAsync(user);
                var res   = _mapper.Map<AuthResultDto>(user);
                res.Token = token;
                res.RefreshToken = await CreateRefreshToken(user);
                res.IsAdmin = await _userManager.IsInRoleAsync(user, "Admin");

                return res;
            }

            throw new InvalidLoginException("Invalid email or password");
        }

        public async Task<UserRegisterResultDto> RegisterAsync(UserRegisterDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            var result = await _userManager.CreateAsync(user, userDto.Password);
            var regResult = new UserRegisterResultDto
            {
                Errors = null
            };

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "USER");
            }

            return regResult;
        }

        public async Task<bool> VerifyRefreshToken(AuthResultDto authResultDto)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(authResultDto.Token);
            var userId = tokenContent.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            if (userId == null)
            {
                throw new InvalidTokenException("Invalid token");
            }

            var user = await _userManager.FindByIdAsync(userId);

            if(user == null)
            {
                throw new UserNotFoundException($"User with id {userId} not found");
            }

            var isValidRefreshToken = await _userManager.VerifyUserTokenAsync(
                user,
                _loginProvider, 
                _tokenName,
                authResultDto.RefreshToken
            );

            return isValidRefreshToken;
        }

        public async Task<string> CreateRefreshToken(User user)
        {
            await _userManager.RemoveAuthenticationTokenAsync(
                user, 
                _loginProvider, 
                _tokenName
                );

            var newRefreshToken = await _userManager.GenerateUserTokenAsync(
                user, 
                _loginProvider, 
                _tokenName
                );

            var result = await _userManager.SetAuthenticationTokenAsync(
                user, 
                _loginProvider, 
                _tokenName,
                newRefreshToken
                );

            return newRefreshToken;
        }
    }
}
