using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using server.SRC.Services;
using server.SRC.Models;
namespace server.SRC.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IRoomMessageService _roomMessageService;

        public ChatHub(IRoomMessageService roomMessageService)
        {
            this._roomMessageService = roomMessageService;
        }
        public async Task JoinRoom(string roomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
        }

        public async Task LeaveRoom(string roomId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);
        }

        public async Task SendMessageToRoom(string roomId, string user, string message)
        {
            //await this._roomMessageService.Save(new RoomMessage(roomId, user, message));
            await Clients.Group(roomId).SendAsync("ReceiveMessage", user, message);
        }
    }
}