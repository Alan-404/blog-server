
namespace server.SRC.DTOs.Requests
{
    public class AddBlogRequest
    {
        public string Title {get; set;}
        public string Introduction {get; set;}
        public string Content {get; set;}
        public IFormFile Thumnail {get; set;}
    }
}