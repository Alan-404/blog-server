using server.SRC.Models;
using Microsoft.EntityFrameworkCore;
using server.SRC.Utils;
using server.SRC.DB;


namespace server.SRC.Services.Providers
{
    public class RoomMessageProvider: IRoomMessageService
    {
        private readonly DatabaseContext _context;

        public RoomMessageProvider(DatabaseContext context)
        {
            this._context = context;
        }

        public async Task<RoomMessage> Save(RoomMessage message)
        {
            try
            {
                message.CreatedAt = DateTime.Now;
                await this._context.RoomMessages.AddAsync(message);
                await this._context.SaveChangesAsync();
                return message;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<List<RoomMessage>> GetByRoomId (string roomId)
        {
            try
            {
                return await this._context.RoomMessages.Where(p => p.RoomId == roomId).ToListAsync();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return new List<RoomMessage>();
            }
        }
    }
}