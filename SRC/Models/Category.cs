using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.SRC.Models
{
    [Table("category")]
    public class Category
    {
        [Key]
        [Column("id")]
        public string Id {get; set;}
        [Column("name")]
        public string Name {get; set;}
    }
}