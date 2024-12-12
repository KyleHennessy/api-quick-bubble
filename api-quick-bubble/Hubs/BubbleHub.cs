using Application.Services.Interfaces;
using Domain;
using Microsoft.AspNetCore.SignalR;

namespace api_quick_bubble.Hubs
{
    public class BubbleHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            Clients.Caller.SendAsync("Connected", Context.ConnectionId);
            return base.OnConnectedAsync();
        }
    }
}
