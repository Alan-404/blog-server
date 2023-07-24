

namespace server.SRC.DTOs.Requests
{
    public class UserRegisterRequest
    {
        public string Email {get; set;}
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string Password {get; set;}
        public string Role {get; set;}
    }

    public class UpdateAvatarRequest
    {
        public IFormFile Avatar {get; set;}
    }
}