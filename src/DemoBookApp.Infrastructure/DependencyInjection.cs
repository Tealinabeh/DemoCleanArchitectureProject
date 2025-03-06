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
using StackExchange.Redis;

namespace DemoBookApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection service, IConfiguration configurations)
    {
        service.AddPersistence(configurations)
                .AddIdentity()
                .AddJWTAuthentication(configurations)
                .AddAuthorization()
                .AddCaching(configurations)
                .AddRepositories();

        return service;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection service, IConfiguration configurations)
    {
        service.AddDbContext<ApplicationDBContext>(options =>
        {
            options.UseSqlite(configurations.GetConnectionString("Database"));
        });
        return service;
    }
    private static IServiceCollection AddCaching(this IServiceCollection services, IConfiguration configurations)
    {

        services.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            var configuration = configurations.GetConnectionString("Redis");
            return ConnectionMultiplexer.Connect(configuration);
        });

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configurations.GetConnectionString("Redis");
            options.InstanceName = "RedisInstance"; 
        });

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection service)
    {
        service.AddScoped<IBookRepository, BookCachedRepository>();
        service.AddScoped<IAuthorRepository, AuthorCachedRepository>();

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
    private static IServiceCollection AddAuthorization(this IServiceCollection service)
    {
        service.AddAuthorization(options =>
        {
            options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
        });

        return service;
    }
}
