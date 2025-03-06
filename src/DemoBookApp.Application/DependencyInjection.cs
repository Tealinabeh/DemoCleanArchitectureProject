using DemoBookApp.Application.Handlers;
using DemoBookApp.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DemoBookApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddHandlers();
        services.AddScoped<ITokenService, JwtTokenService>();

        return services;
    }

    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<IAuthorHandler, AuthorRequestHandler>();
        services.AddScoped<IBookHandler, BookRequestHandler>();
        services.AddScoped<IAccountHandler, AccountHandler>();
        services.AddScoped<IUserHandler, UserHandler>();

        return services;
    }
}