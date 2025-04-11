using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ShortURL.API.Domain.DbContexts;
using ShortURL.API.Domain.Models;
using ShortURL.API.DTOs.Configurations;
using ShortURL.API.Exceptions.Configuration;
using ShortURL.API.Repositories;
using ShortURL.API.Services;
using System.Text;

namespace ShortURL.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ShortURL API",
                    Version = "v1",
                    Description = "API for Shortened URL Service"
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \n\r
                                    Enter 'Bearer' [space] and then your token in the text input below. \n\r
                                    Example: 'Bearer gdkk45f'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });


            builder.Services.AddDbContext<ShortUrlDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ShortUrlDbConnection")));

            builder.Services.AddScoped<IShortUrlRepository, ShortUrlRepository>();
            builder.Services.AddScoped<IStaticContentRepository, StaticContentRepository>();

            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IShortUrlService, ShortUrlService>();
            builder.Services.AddScoped<IStaticContentService, StaticContentService>();

            builder.Services.AddAutoMapper(typeof(MapperConfig));


            builder.Services.AddIdentityCore<User>(options =>
            {
                // ������������ ������������
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_";
                // ������������ ������
                options.Password.RequireDigit = true; 
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false; 
                options.Password.RequireUppercase = true; 
                options.Password.RequireLowercase = true;

                
            })
                .AddRoles<IdentityRole<int>>()
                .AddTokenProvider<DataProtectorTokenProvider<User>>("ShortUrlAPI")
                .AddEntityFrameworkStores<ShortUrlDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
                        ),
                    ValidateIssuer   = true,
                    ValidateLifetime = true,
                    ValidateAudience = true,
                    ClockSkew        = TimeSpan.Zero,
                    ValidIssuer      = builder.Configuration["Jwt:Issuer"],
                    ValidAudience    = builder.Configuration["Jwt:Audience"]
                };
            });
       

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("DevelopmentPolicy",
                    policy => policy
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        );
            });

            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseExceptionHandling();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();
         

            app.MapControllers();

            app.UseCors("DevelopmentPolicy");

            app.Run();
        }
    }
}
