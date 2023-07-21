

namespace server.SRC.DTOs.Responses
{
    public class LoginAccountResponse
    {
        public string AccessToken {get; set;}

        public LoginAccountResponse(){}
        public LoginAccountResponse(string accessToken)
        {
            this.AccessToken = accessToken;
        }
    }
    public class GetInfoByTokenResponse
    {
        public string UserId {get; set;}
        public string Email {get; set;}
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string Role {get; set;}

        public GetInfoByTokenResponse(){}
        public GetInfoByTokenResponse(string userId, string email, string firstName, string lastName, string role)
        {
            this.UserId = userId;
            this.Email = email;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Role = role;
        }
    }
}