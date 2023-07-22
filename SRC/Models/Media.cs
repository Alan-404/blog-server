using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.SRC.Models
{
    [Table("media")]
    public class Media
    {
        [Key]
        [Column("id")]
        public string Id {get; set;}
        [Column("blog_id")]
        public string BlogId {get; set;}
        public Media(){}

        public Media(string blogId)
        {
            this.BlogId = blogId;
        }
    }
}