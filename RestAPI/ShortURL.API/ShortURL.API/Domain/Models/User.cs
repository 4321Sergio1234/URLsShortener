using Microsoft.AspNetCore.Identity;

namespace ShortURL.API.Domain.Models
{
    public class User: IdentityUser<int>
    {
        public virtual IList<ShortUrl>? ShortUrls { get; set; } = [];
    }
}
