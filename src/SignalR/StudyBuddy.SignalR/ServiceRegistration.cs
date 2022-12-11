using Microsoft.AspNetCore.SignalR;
using StudyBuddy.SignalR.Helpers;
using StudyBuddy.SignalR.Services;

namespace StudyBuddy.SignalR;

public static class ServiceRegistration
{
    public static void AddSignalRServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<CustomTokenOption>(configuration.GetSection("TokenOption"));
        var tokenOptions = configuration.GetSection("TokenOption").Get<CustomTokenOption>();
        services.AddCustomTokenAuth(tokenOptions);
        services.AddSingleton<IUserIdProvider, UserNameProvider>();
        services.AddSingleton<TwilioService>();
    }
}