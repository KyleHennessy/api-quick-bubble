using Domain;
using Microsoft.AspNetCore.SignalR;

namespace api_quick_bubble.Hubs
{
    public class BubbleHub : Hub
    {
        public async Task SendMessage(Bubble bubble)
        {
            await Clients.Others.SendAsync("ReceiveMessage", bubble);
        }
    }
}
