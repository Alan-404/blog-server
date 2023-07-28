using server.SRC.Models;

namespace server.SRC.Services
{
    public interface IRoomService
    {
        public Task<Room> Save(Room room);
        public Task<Room> GetById(string id);
        public Task<List<Room>> FindByUserId(string userId);
        public Task<Room> GetByTwoUsers (string user1, string user2);
    }
}