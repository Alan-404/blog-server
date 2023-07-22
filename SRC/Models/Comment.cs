using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.SRC.Models
{
    [Table("comment")]
    public class Comment
    {
        [Key]
        [Column("id")]
        public string Id {get; set;}
        [Column("user_id")]
        public string UserId {get; set;}
        [Column("blog_id")]
        public string BlogId {get; set;}
        [Column("reply")]
        public string Reply {get; set;}
        [Column("content")]
        public string Content {get; set;}
        [Column("created_at")]
        public DateTime CreatedAt {get; set;}
        [Column("modified_at")]
        public DateTime ModifiedAt {get; set;}
        public Comment(){}

        public Comment(string userId, string reply, string content)
        {
            this.UserId = userId;
            this.Reply =reply;
            this.Content = content;
        }
    }
}