using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace server.SRC.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string message, string sender)
        {
            Console.WriteLine(message);
            await Clients.All.SendAsync("ChatMessage", message, sender);
        }
    }
}