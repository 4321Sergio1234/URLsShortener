namespace ShortURL.API.DTOs.ShortUrlDtos
{
    public class CreateShortUrlDto
    {
        public required string OriginalUrl { get; set; }
        public required int    UserId { get; set; }
    }
}
