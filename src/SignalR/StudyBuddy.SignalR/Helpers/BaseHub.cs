using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;

namespace StudyBuddy.SignalR.Helpers;

public class BaseHub : Hub
{
    protected BaseHub(HubConnectionContext connection)
    {
        UserName=connection.User?.FindFirst(ClaimTypes.Name)?.Value!;
    }

    protected string UserName { get; set; }
}