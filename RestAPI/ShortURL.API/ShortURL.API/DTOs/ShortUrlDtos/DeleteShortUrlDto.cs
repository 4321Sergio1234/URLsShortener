namespace ShortURL.API.DTOs.ShortUrlDtos
{
    public class DeleteShortUrlDto
    {
        public required int ShortUrlId { get; set; }
        public required int UserId     { get; set; }
    }
}
