using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using StudyBuddy.SignalR.Helpers;
using StudyBuddy.SignalR.Models;

namespace StudyBuddy.SignalR.Hubs;

[Authorize]
public class ClassroomHub :  Hub
{
    // public ClassroomHub(HubConnectionContext connection) : base(connection)
    // {
    // }
    private readonly IUserIdProvider _user;

    public ClassroomHub(IUserIdProvider user)
    {
        _user = user;
    }

    public async Task JoinClassroom(string id)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, id);
        //await Clients.OthersInGroup(id).SendAsync($"Notification-{id}","Emre has joined",id);
        var userId = Context.User.Claims.Where(x=>x.Type==ClaimTypes.NameIdentifier).SingleOrDefault().Value;
        await Clients.Group(id).SendAsync($"Notification",$"{Context.UserIdentifier}",userId);
    }
    
    public async Task LeaveClassroom(string id)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, id);
    }

    public async Task Send(Message m)
    {
        var userId = Context.User.Claims.Where(x=>x.Type==ClaimTypes.NameIdentifier).SingleOrDefault().Value;
        await Clients.Group(m.GroupId).SendAsync("ReceiveMessage", m,$"{Context.UserIdentifier}",userId);
        
    }
    
}