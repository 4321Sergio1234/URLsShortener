namespace ShortURL.API.DTOs.ShortUrlDtos
{
    public class ViewRestrictedShortUrlDto
    {
        public required string Id { get; set; }
        public required string OriginalUrl { get; set; }
        public required string ShortenedUrl { get; set; }
    }
}
