

namespace server.SRC.DTOs.Responses
{
    public class ShowBlogsResponse
    {
        public List<BlogInfo> blogs {get; set;}
        public int TotalItems {get; set;}
        public int TotalPages {get; set;}
    }

    public class BlogDetail
    {
        public string Author {get; set;}
        public string BlogId {get; set;}
        public string Title{get; set;}
        public string Introduction {get; set;}
        public string Content {get; set;}
        public int NumComments {get; set;}
        public int numViews {get; set;}
        public DateTime CreatedAt {get; set;}
        public DateTime ModifiedAt {get; set;}
    }

    public class AddMediaResponse{
        public string MediaId {get; set;}

        public AddMediaResponse(string mediaId)
        {
            this.MediaId = mediaId;
        }
    }
    public class BlogInfo
    {
        public string Author {get; set;}
        public string BlogId {get; set;}
        public string Title{get; set;}
        public string Introduction {get; set;}
        public int NumComments {get; set;}
        public int numViews {get; set;}
        public DateTime CreatedAt {get; set;}
        public DateTime ModifiedAt {get; set;}
    }
}