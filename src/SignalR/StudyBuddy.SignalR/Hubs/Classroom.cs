using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace StudyBuddy.SignalR.Hubs;

[Authorize]
public class ClassroomHub :  Hub
{
    public async Task JoinClassroom(string id)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, id);
    }

    public async Task LeaveClassroom(string id)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, id);
    }
}