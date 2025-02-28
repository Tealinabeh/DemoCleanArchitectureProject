using DemoBookApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DemoBookApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection service, IConfiguration configurations)
    {
        service.AddPersistence(configurations);
        
        return service;
    }

    public static IServiceCollection AddPersistence(this IServiceCollection service, IConfiguration configurations)
    {
        service.AddDbContext<ApplicationDBContext>(options =>{
            options.UseSqlite(configurations.GetConnectionString(nameof(ApplicationDBContext)));
        });
        return service;
    }
}
