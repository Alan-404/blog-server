using server.SRC.Models;

namespace server.SRC.Services
{
    public interface IRoomService
    {
        public Task<Room> Save(Room room);
    }
}