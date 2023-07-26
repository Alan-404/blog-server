

namespace server.SRC.DTOs.Requests
{
    public class AddSocialNetworkRequest
    {
        public string Name {get; set;}
        public IFormFile File {get; set;}
    }

    public class SocialNetworkInfo
    {
        public int NetworkId {get; set;}
        public string Link {get; set;}

        public SocialNetworkInfo(){}

        public SocialNetworkInfo(int networkId, string link)
        {
            this.NetworkId = networkId;
            this.Link = link;
        }
    }
}