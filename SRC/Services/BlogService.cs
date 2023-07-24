using server.SRC.Models;

namespace server.SRC.Services
{
    public interface IBlogService
    {
        public Task<Blog> Save (Blog blog);
        public Task<Blog> Edit (Blog blog);
        public Task<List<Blog>> GetAll();
        public Task<Blog> GetById (string id);
        public Task<bool> SaveThumnail(IFormFile thumnail, string id);
        public Task<List<Blog>> Paginate(int page, int num);
        public string GetThumnailLink(string id);
        public string GetMediaLink(string id);
        public Task<string> SaveMedia(IFormFile file);
    }
}