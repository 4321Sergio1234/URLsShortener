using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShortURL.API.Domain.Configurations;
using ShortURL.API.Domain.Models;

namespace ShortURL.API.Domain.DbContexts
{
    public class ShortUrlDbContext: IdentityDbContext<User,IdentityRole<int>,int>
    {
        public ShortUrlDbContext(DbContextOptions<ShortUrlDbContext> options) : base(options)
        {
        }
        
        public required DbSet<ShortUrl>      ShortUrls     { get; set; }
        public override required DbSet<User> Users         { get; set; }
        public required DbSet<StaticContent> StaticContents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ShortUrl>((entity) =>
            {
                entity.HasKey(s => s.Id);
                entity.HasIndex(s => s.ShortenedUrl).IsUnique();
                entity.HasIndex(s => s.OriginalUrl).IsUnique();
                entity.HasOne(s => s.User)
                      .WithMany(u => u.ShortUrls)
                      .HasForeignKey(s => s.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.Property(s => s.CreatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(s => s.OriginalUrl)
                      .HasMaxLength(1000)
                      .IsRequired();
            });


            modelBuilder.Entity<StaticContent>((entity) =>
            {
                entity.HasKey(s => s.Id);
                entity.HasIndex(s => s.PageTag).IsUnique();
                entity.Property(s => s.CreatedAt)
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserRolesConfiguration());
            modelBuilder.ApplyConfiguration(new StaticContentConfiguration());
        }
    }
    
    
}
