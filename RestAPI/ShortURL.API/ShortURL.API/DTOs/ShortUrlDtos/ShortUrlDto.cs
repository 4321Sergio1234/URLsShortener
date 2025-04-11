using ShortURL.API.DTOs.UserDtos;

namespace ShortURL.API.DTOs.ShortUrlDtos
{
    public class ShortUrlDto
    {
        public int             Id { get; set; }
        public required string OriginalUrl { get; set; }
        public string          ShortenedUrl { get; set; }
        public int             UserId { get; set; }
        public UserDto?        User { get; set; }
        public DateTime        CreatedAt { get; set; }
    }
}
