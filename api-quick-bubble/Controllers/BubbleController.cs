using api_quick_bubble.Hubs;
using Application.Services.Interfaces;
using Domain;
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
        private readonly ILogger<BubbleController> _logger;

        public BubbleController(IHubContext<BubbleHub> hubContext, IImageCompressor imageCompressor, ILogger<BubbleController> logger)
        {
            _hubContext = hubContext;
            _imageCompressor = imageCompressor;
            _logger = logger;
        }

        [HttpPost("send/{connectionId}")]
        public async Task<ActionResult<Bubble>> SendBubble(string connectionId, [FromBody] Bubble bubble)
        {
            try
            {
                if (bubble.Background != null)
                {
                    bubble.Background = _imageCompressor.CompressImage(bubble.Background, 20);
                }

                await _hubContext.Clients.AllExcept(connectionId).SendAsync("NewBubble", bubble);

                return Ok(bubble);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error has occurred: {ex.Message}");
                return BadRequest();
            }
        }
    }
}
