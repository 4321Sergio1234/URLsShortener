using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShortURL.API.DTOs.UserDtos
{
    public class UserRegisterDto
    {
        [MaxLength(20)]
        [MinLength(1)]
        public required string UserName { get; set; }
        [MinLength(8)]
        [MaxLength(20)]
        public required string Password { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
    }
}
