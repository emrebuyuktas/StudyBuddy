using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using StudyBuddy.Application.Helpers;

namespace StudyBuddy.Application;

public static class ServiceRegistration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        var assembly=Assembly.GetExecutingAssembly();
        services.AddAutoMapper(assembly);
        services.AddMediatR(assembly);
        services.AddScoped<ITokenService, TokenService>();
        services.AddStackExchangeRedisCache(action =>
        {
            action.Configuration = "studybuddyrds.redis.cache.windows.net:6380,password=KCWRkak4UbRia7YZqJthGao4OnC2MZ7BhAzCaC0zJEM=,ssl=True,abortConnect=False";
        });
        
    }
}