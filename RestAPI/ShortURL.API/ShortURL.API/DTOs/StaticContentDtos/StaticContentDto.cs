using System.ComponentModel.DataAnnotations;

namespace ShortURL.API.DTOs.StaticContentDtos
{
    public class StaticContentDto
    {
        [MaxLength(128)]
        [MinLength(5)]
        public required string Title { get; set; }
        [MaxLength(524)]
        public required string Content { get; set; }
    }
}
