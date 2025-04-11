namespace ShortURL.API.Domain.Models
{
    public class ShortUrl
    {
        public int             Id           { get; set; }
        public required string OriginalUrl  { get; set; }
        public string          ShortenedUrl { get; set; }
        public required int    UserId       { get; set; }
        public virtual  User?  User         { get; set; }
        public DateTime        CreatedAt    { get; set; }
    }
}
