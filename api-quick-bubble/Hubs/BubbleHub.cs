using Application.Services.Interfaces;
using Domain;
using Microsoft.AspNetCore.SignalR;

namespace api_quick_bubble.Hubs
{
    public class BubbleHub : Hub
    {
        private readonly IImageCompressor _imageCompressor;

        public BubbleHub(IImageCompressor imageCompressor)
        {
            _imageCompressor = imageCompressor;
        }
        public async Task SendMessage(Bubble bubble)
        {
            if (bubble.Background != null)
            {
                bubble.Background = _imageCompressor.CompressImage(bubble.Background, 20);
            }

            await Clients.Others.SendAsync("ReceiveMessage", bubble);
        }
    }
}
