using DemoBookApp.Application.Handlers;
using DemoBookApp.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DemoBookApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthorHandler, AuthorRequestHandler>();
        services.AddScoped<IBookHandler, BookRequestHandler>();

        return services;
    }
}