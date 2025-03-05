using System.Text;
using DemoBookApp.Core;
using DemoBookApp.Infrastructure.Interfaces;
using DemoBookApp.Infrastructure.Persistence;
using DemoBookApp.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace DemoBookApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection service, IConfiguration configurations)
    {
        service.AddPersistence(configurations)
                .AddIdentity()
                .AddJWTAuthentication(configurations)
                .AddCustomAuthorization()
                .AddRepositories();

        return service;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection service, IConfiguration configurations)
    {
        service.AddDbContext<ApplicationDBContext>(options =>
        {
            options.UseSqlite(configurations.GetConnectionString("Sqlite"));
        });

        return service;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection service)
    {
        service.AddScoped<IBookRepository, BookRepository>();
        service.AddScoped<IAuthorRepository, AuthorRepository>();

        return service;
    }
    private static IServiceCollection AddIdentity(this IServiceCollection service)
    {
        service.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDBContext>()
                .AddDefaultTokenProviders();

        return service;
    }
    private static IServiceCollection AddJWTAuthentication(this IServiceCollection service, IConfiguration configurations)
    {
        var jwtSettings = configurations.GetSection("JwtSettings");

        service.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]))
            };
        });

        return service;
    }
    private static IServiceCollection AddCustomAuthorization(this IServiceCollection service)
    {
        service.AddAuthorization(options =>
        {
            options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
        });

        return service;
    }
}
