using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.SRC.Models
{
    [Table("account")]
    public class Account
    {
        [Key]
        [Column("id")]
        public string Id {get; set;}
        [Column("user_id")]
        public string UserId {get; set;}
        [Column("password")]
        public string Password {get; set;}
        [Column("role")]
        public string Role {get; set;}
        [Column("modified_at")]
        public DateTime ModifiedAt {get; set;}

        public Account(){}

        public Account(string userId, string password, string role)
        {
            this.UserId = userId;
            this.Password = password;
            this.Role = role;
        }
    }
}