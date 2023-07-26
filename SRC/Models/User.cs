using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.SRC.Models
{
    [Table("user_system")]
    public class User
    {
        [Key]
        [Column("id")]
        public string Id {get; set;}
        [Column("email")]
        public string Email {get; set;}
        [Column("first_name")]
        public string FirstName {get; set;}
        [Column("last_name")]
        public string LastName {get; set;}
        [Column("phone_number")]
        public string PhoneNumber {get; set;}
        [Column("b_date")]
        public DateTime? BDate {get; set;}
        [Column("gender")]
        public string Gender {get; set;}
        [Column("created_at")]
        public DateTime CreatedAt {get; set;}
        [Column("modified_at")]
        public DateTime ModifiedAt {get; set;}

        public User() {}
        public User(string email , string firstName, string lastName)
        {
            this.Email = email;
            this.FirstName = firstName;
            this.LastName = lastName;
        }
    }
}
