using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudyBuddy.Application.Helpers;
using StudyBuddy.Application.Interfaces;
using StudyBuddy.Persistence.Context;

namespace StudyBuddy.Persistence;

public static class ServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
    {
        var assembly=Assembly.GetExecutingAssembly();
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("LocalDB"), sqlOptions =>
            {
                sqlOptions.MigrationsAssembly("StudyBuddy.Persistence");
            });
        });
        services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
        services.AddStackExchangeRedisCache(action =>
        {
            action.Configuration = "localhost:6379";
        });
        
    }
}