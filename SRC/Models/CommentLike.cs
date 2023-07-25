using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.SRC.Models
{
    [Table("comment_like")]
    public class CommentLike
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set;}
        [Column("comment_id")]
        public string CommentId {get; set;}
        [Column("user_id")]
        public string UserId {get;set;}

        public CommentLike(){}

        public CommentLike(string commentId, string userId)
        {
            this.CommentId = commentId;
            this.UserId = userId;
        }
    }
}