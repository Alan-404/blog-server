using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.SRC.Models
{
    [Table("room_message")]
    public class RoomMessage
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set;}
        [Column("room_id")]
        public string RoomId {get; set;}
        [Column("user_id")]
        public string UserId {get; set;}
        [Column("content")]
        public string Content {get; set;}
        [Column("created_at")]
        public DateTime CreatedAt {get; set;}
        [Column("status")]
        public bool Status {get; set;}

        public RoomMessage(){}

        public RoomMessage(string roomId, string userId, string content)
        {
            this.RoomId = roomId;
            this.UserId = userId;
            this.Content = content;
        }
    }
}