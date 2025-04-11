using AutoMapper;
using ShortURL.API.Domain.Models;
using ShortURL.API.DTOs.ShortUrlDtos;
using ShortURL.API.DTOs.StaticContentDtos;
using ShortURL.API.DTOs.UserDtos;


namespace ShortURL.API.DTOs.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<User, AuthResultDto>();
            CreateMap<UserRegisterDto, User>();
            CreateMap<User, UserDto>();
            CreateMap<ShortUrl, ShortUrlDto>().ReverseMap();
            CreateMap<ShortUrl, ViewRestrictedShortUrlDto>();
            CreateMap<CreateShortUrlDto, ShortUrl>().ReverseMap();
            CreateMap<StaticContent, StaticContentDto>().ReverseMap();
        }
    }
}
