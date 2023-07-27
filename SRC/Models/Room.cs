using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.SRC.Models
{
    [Table("room")]
    public class Room
    {   
        [Key]
        [Column("id")]
        public string Id {get; set;}
        [Column("user_1")]
        public string User1 {get; set;}
        [Column("user_2")]
        public string User2 {get; set;}

        public Room(){}

        public Room(string user1, string user2)
        {
            this.User1 = user1;
            this.User2 = user2;
        }
    }
}