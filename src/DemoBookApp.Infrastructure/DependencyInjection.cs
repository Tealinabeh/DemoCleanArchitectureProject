using DemoBookApp.Infrastructure.Interfaces;
using DemoBookApp.Infrastructure.Persistence;
using DemoBookApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DemoBookApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection service, IConfiguration configurations)
    {
        service.AddPersistence(configurations)
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
}
