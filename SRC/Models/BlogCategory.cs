using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace server.SRC.Models
{
    [Table("blog_category")]
    public class BlogCategory
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get;set;}
        [Column("blog_id")]
        public string BlogId {get; set;}
        [Column("category_id")]
        public string CategoryId {get;set;}
    }
}