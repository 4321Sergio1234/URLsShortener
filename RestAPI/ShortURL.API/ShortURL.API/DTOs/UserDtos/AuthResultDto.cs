namespace ShortURL.API.DTOs.UserDtos
{
    public class AuthResultDto
    {
        public required int    Id { get; set; }
        public required string Token        { get; set; }
        public required string RefreshToken { get; set; }
        public required string UserName     { get; set; }
        public required string Email        { get; set; }
        public bool            IsAdmin      { get; set; }
    }
}
