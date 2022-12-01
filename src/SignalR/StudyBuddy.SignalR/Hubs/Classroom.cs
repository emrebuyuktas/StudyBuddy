using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using StudyBuddy.SignalR.Models;

namespace StudyBuddy.SignalR.Hubs;

[Authorize]
public class ClassroomHub :  Hub
{
    private static Dictionary<string, string> Users = new Dictionary<string, string>();
    public async Task JoinClassroom(string id)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, id);
        //await Clients.OthersInGroup(id).SendAsync($"Notification-{id}","Emre has joined",id);
        await Clients.Group(id).SendAsync($"Notification-{id}","Emre has joined",id);
    }
    
    public async Task LeaveClassroom(string id)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, id);
    }

    public async Task Send(Message m)
    {
        await Clients.Group(m.GroupId).SendAsync("ReceiveMessage", m);
    }
    
}