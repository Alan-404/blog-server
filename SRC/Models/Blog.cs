using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace server.SRC.Models
{
    [Table("blog")]
    public class Blog
    {
        [Key]
        [Column("id")]
        public string Id {get; set;}
        [Column("user_id")]
        public string UserId {get; set;}
        [Column("title")]
        public string Title {get; set;}
        [Column("introduction")]
        public string Introduction {get; set;}
        [Column("content")]
        public string Content {get; set;}
        [Column("created_at")]
        public DateTime CreatedAt {get; set;}
        [Column("modified_at")]
        public DateTime ModifiedAt {get; set;}

        public Blog(){}

        public Blog(string userId ,string title, string introduction, string content)
        {
            this.UserId = userId;
            this.Title = title;
            this.Introduction = introduction;
            this.Content = content;
        }
    }
}