using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;

namespace StudyBuddy.SignalR.Helpers;


public class UserNameProvider : IUserIdProvider
{
    public string? GetUserId(HubConnectionContext connection)
    {
        return connection.User?.FindFirst(ClaimTypes.Name)?.Value!;
    }
}