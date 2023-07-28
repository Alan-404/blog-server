using server.SRC.Models;

namespace server.SRC.DTOs.Responses
{
    public class RoomInfo
    {
        public string RoomId {get; set;}
        public User Sender {get; set;}
        public User Receiver {get; set;}

        public RoomInfo(){}

        public RoomInfo(string roomId, User sender, User receiver)
        {
            this.RoomId = roomId;
            this.Sender = sender;
            this.Receiver = receiver;
        }
    }
}