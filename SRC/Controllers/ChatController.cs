using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using server.SRC.Hubs;
using server.SRC.DTOs.Requests;
namespace server.SRC.Controllers
{
    [ApiController]
    [Route("server/chat")]
    public class ChatController: ControllerBase
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatController(IHubContext<ChatHub> hubContext)
        {
            this._hubContext = hubContext;
        }

        [HttpPost]
        public async Task<ActionResult> SendMessage([FromBody] MessageDTO message)
        {
            await this._hubContext.Clients.All.SendAsync("ChatMessage", message.User, message.Message);

            

            return Ok();

        }
    }
}