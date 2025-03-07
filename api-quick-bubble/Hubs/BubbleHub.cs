using Application.Services.Interfaces;
using Domain;
using Microsoft.AspNetCore.SignalR;

namespace api_quick_bubble.Hubs
{
    public class BubbleHub : Hub<IBubbleClient>
    {
        private readonly IConnectionCounter _connectionCounter;

        public BubbleHub(IConnectionCounter connectionCounter)
        {
            _connectionCounter = connectionCounter;
        }

        public async Task NewBubble(Bubble bubble)
        {
            await Clients.Others.NewBubble(bubble);
        }

        public async Task ConnectionUpdate(int count)
        {
            await Clients.All.ConnectionUpdate(count);
        }

        public override async Task OnConnectedAsync()
        {
            var count = _connectionCounter.IncrementConnectionCount();

            await ConnectionUpdate(count);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var count = _connectionCounter.DecrementConnectionCount();

            await ConnectionUpdate(count);
        }
    }
}
