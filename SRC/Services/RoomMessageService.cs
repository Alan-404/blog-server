using server.SRC.Models;

namespace server.SRC.Services
{
    public interface IRoomMessageService
    {
        public Task<RoomMessage> Save(RoomMessage message);
        public Task<List<RoomMessage>> GetByRoomId (string roomId);
    }
}