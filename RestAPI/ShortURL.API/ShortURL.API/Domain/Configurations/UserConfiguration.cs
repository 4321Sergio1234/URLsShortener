using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShortURL.API.Domain.Models;

namespace ShortURL.API.Domain.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User
                {
                    Id = 1,
                    UserName = "AdminName",
                    NormalizedUserName = "ADMINNAME",
                    Email = "admin@example.com",
                    NormalizedEmail = "ADMIN@EXAMPLE.COM",
                    EmailConfirmed = false,
                    PasswordHash = "AQAAAAIAAYagAAAAENOx6qSXtPoZKSSfB7y7/uxZ0c1chtuelSA59P0WhppXB95jMitKk2K/eO8W4r+Xig==", // Aa123456
                    SecurityStamp = "AJFRID33UTWLWJVO7KCF6R427XWCCHFG",
                    ConcurrencyStamp = "2e8e3f18-a52b-4d6f-ac50-d809e7e3b9a9",
                    PhoneNumber = null,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                },
                new User
                {
                    Id = 2,
                    UserName = "UserName",
                    NormalizedUserName = "USERNAME",
                    Email = "user@example.com",
                    NormalizedEmail = "USER@EXAMPLE.COM",
                    EmailConfirmed = false,
                    PasswordHash = "AQAAAAIAAYagAAAAEKj+jltWKBUmgFykMexvzNoukn9fKlpjugUhQ9KtrULxfoQKLluIf5tVNc9ccouRIg==", // Uu123456
                    SecurityStamp = "DOPK5LMRGC3BTYIML3Y3SWUAI5AKIL5Y",
                    ConcurrencyStamp = "0401242c-2dea-4c6e-a9b1-ae784ba328ce",
                    PhoneNumber = null,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                }
            );
        }
    }
}
