namespace server.SRC.DTOs.Requests
{
    public class MessageDTO
    {
        public string Message {get; set;}
        public string User {get; set;}

        public MessageDTO(){}

        public MessageDTO(string user, string message)
        {
            this.User = user;
            this.Message = message;
        }
    }
}