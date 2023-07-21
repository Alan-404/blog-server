using server.SRC.Models;

namespace server.SRC.Services
{
    public interface IBlogService
    {
        public Task<Blog> Save (Blog blog);
        public Task<List<Blog>> GetAll();
        public Task<Blog> GetById (string id);
        public Task<bool> SaveThumnail(IFormFile thumnail, string id);
        public string GetThumnailLink(string id);
    }
}