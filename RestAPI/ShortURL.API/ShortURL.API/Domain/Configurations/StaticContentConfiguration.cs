using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShortURL.API.Domain.Models;

namespace ShortURL.API.Domain.Configurations
{
    public class StaticContentConfiguration : IEntityTypeConfiguration<StaticContent>
    {
        public void Configure(EntityTypeBuilder<StaticContent> builder)
        {
            builder.HasData(
                new StaticContent
                {
                    Id = 1,
                    PageTag = "about-view",
                    Title = "About ShortenedUrl",
                    Content = "ShortenedUrl is a URL shortening service that allows users to create short, easy-to-share links for long URLs. It is designed to make sharing links easier and more convenient, especially on social media platforms where character limits may apply.",
                    CreatedAt = DateTime.UtcNow,
                }
            );
        }
    }
}
