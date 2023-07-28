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

        public async Task<List<Room>> FindByUserId(string userId)
        {
            try
            {
                return await this._context.Rooms.Where(p => p.User1 == userId || p.User2 == userId).ToListAsync();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<Room> GetById(string id)
        {
            try
            {
                return await this._context.Rooms.FindAsync(id);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<Room> GetByTwoUsers (string user1, string user2)
        {
            try
            {
                return await this._context.Rooms.FirstOrDefaultAsync(p => p.User1 == user1 && p.User2 == user2);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}