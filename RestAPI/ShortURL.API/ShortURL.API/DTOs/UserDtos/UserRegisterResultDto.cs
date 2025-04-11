using Microsoft.AspNetCore.Identity;

namespace ShortURL.API.DTOs.UserDtos
{
    public class UserRegisterResultDto
    {
        public IEnumerable<IdentityError>? Errors;
    }
}
