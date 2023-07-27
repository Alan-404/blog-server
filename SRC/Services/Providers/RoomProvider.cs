using Microsoft.EntityFrameworkCore;
using server.SRC.Models;
using server.SRC.DB;
using server.SRC.Utils;


namespace server.SRC.Services.Providers
{
    public class RoomProvider: IRoomService
    {
        private readonly DatabaseContext _context;

        public RoomProvider(DatabaseContext context)
        {
            this._context = context;
        }

        public async Task<Room> Save(Room room)
        {
            try
            {
                room.Id = Library.GenerateId(21);
                await this._context.Rooms.AddAsync(room);
                await this._context.SaveChangesAsync();
                return room;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}