using DemoBookApp.Application.Decorators;
using DemoBookApp.Application.Interfaces;
using DemoBookApp.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DemoBookApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthorRepository, AuthorPersistenceDecorator>();
        services.AddScoped<IBookHandler, BookPersistenceHandler>();

        return services;
    }
}