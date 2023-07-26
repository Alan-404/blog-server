using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace server.SRC.Models
{
    [Table("social_network")]
    public class SocialNetwork
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set;}
        [Column("name")]
        public string Name {get; set;}

        public SocialNetwork(){}

        public SocialNetwork(string name)
        {
            this.Name = name;
        }
    }
}