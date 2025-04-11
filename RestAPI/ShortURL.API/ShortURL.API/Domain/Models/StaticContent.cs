using System.ComponentModel.DataAnnotations;

namespace ShortURL.API.Domain.Models
{
    public class StaticContent
    {
        public int             Id        { get; set; }
        [MaxLength(50)]
        public required string PageTag   { get; set; }
        [MaxLength(128)]
        [MinLength(5)]
        public required string Title     { get; set; }
        [MaxLength(524)]
        public required string Content   { get; set; }
        public DateTime        CreatedAt { get; set; }
    }
}
