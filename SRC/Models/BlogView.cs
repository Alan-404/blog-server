using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace server.SRC.Models
{
    [Table("blog_view")]
    public class BlogView
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set;}
        [Column("user_id")]
        public string UserId {get; set;}
        [Column("blog_id")]
        public string BlogId {get; set;}
        [Column("num")]
        public int Num {get; set;}

        public BlogView(){}

        public BlogView(string userId, string blogId, int num)
        {
            this.UserId = userId;
            this.BlogId = blogId;
            this.Num = num;
        }
    }
}