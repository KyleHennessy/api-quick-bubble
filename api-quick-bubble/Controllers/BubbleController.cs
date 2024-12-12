using api_quick_bubble.Hubs;
using Application.Services.Interfaces;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace api_quick_bubble.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BubbleController : ControllerBase
    {
        private readonly IHubContext<BubbleHub> _hubContext;

        private readonly IImageCompressor _imageCompressor;

        public BubbleController(IHubContext<BubbleHub> hubContext, IImageCompressor imageCompressor)
        {
            _hubContext = hubContext;
            _imageCompressor = imageCompressor;
        }

        [HttpPost("send/{connectionId}")]
        public async Task SendBubble(string connectionId, [FromBody] Bubble bubble)
        {
            if (bubble.Background != null)
            {
                bubble.Background = _imageCompressor.CompressImage(bubble.Background, 20);
            }

            await _hubContext.Clients.AllExcept(connectionId).SendAsync("ReceiveMessage", bubble);
        }
    }
}
