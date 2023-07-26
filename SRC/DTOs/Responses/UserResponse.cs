using server.SRC.DTOs.Requests;

namespace server.SRC.DTOs.Responses
{
    public class UserInfo
    {
        public string Id {get; set;}
        public string Email {get; set;}
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string PhoneNumber {get; set;}
        public DateTime? BDate {get; set;}
        public string Gender {get; set;}
        public List<SocialNetworkInfo> Networks {get; set;}
        public DateTime CreatedAt {get; set;}
        public DateTime ModifiedAt {get; set;}

        public UserInfo(){}

        public UserInfo(string id, string email, string firstName, string lastName, string phoneNumber, DateTime? bDate, string gender, List<SocialNetworkInfo> networks, DateTime createdAt, DateTime modifiedAt)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.BDate = bDate;
            this.Gender = gender;
            this.Networks = networks;
            this.CreatedAt = createdAt;
            this.ModifiedAt = modifiedAt;
        }
    }
}