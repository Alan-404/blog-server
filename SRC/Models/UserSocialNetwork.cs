using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.SRC.Models
{
    [Table("user_social_network")]
    public class UserSocialNetwork
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set;}
        [Column("user_id")]
        public string UserId {get; set;}
        [Column("social_network_id")]
        public int NetworkId {get; set;}
        [Column("link")]
        public string Link {get; set;}

        public UserSocialNetwork(){}

        public UserSocialNetwork(string userId, int networkId, string link)
        {
            this.UserId = userId;
            this.NetworkId = networkId;
            this.Link = link;
        }
    }
}