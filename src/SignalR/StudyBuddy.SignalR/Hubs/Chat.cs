using Microsoft.AspNetCore.SignalR;
using StudyBuddy.SignalR.Models;

namespace StudyBuddy.SignalR.Hubs;

// public class Chat : Hub<IChatHub>
// {
//     public async Task Send(Message m)
//     {
//         await Clients.Group(m.GroupId).ReceiveMessage(m);
//     }
// }