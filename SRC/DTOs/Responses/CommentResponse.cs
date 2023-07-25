

namespace server.SRC.DTOs.Responses
{
    public class CommentInfo
    {
        public string Id {get; set;}
        public string UserId {get; set;}
        public string Username {get; set;}
        public string BlogId {get; set;}
        public string Reply {get; set;}
        public string Content {get; set;}
        public int NumReplies {get;set;}
        public int NumLikes {get;set;}
        public DateTime CreatedAt {get; set;}
        public DateTime ModifiedAt {get; set;}

        public CommentInfo(){}

        public CommentInfo(string id, string userId, string username, string blogId, string reply, string content, int numReplies, int numLikes, DateTime createdAt, DateTime modifiedAt)
        {
            this.Id = id;
            this.UserId = userId;
            this.Username = username;
            this.BlogId = blogId;
            this.Reply = reply;
            this.Content = content;
            this.NumReplies = numReplies;
            this.NumLikes = numLikes;
            this.CreatedAt = createdAt;
            this.ModifiedAt = modifiedAt;
        }
    }
}