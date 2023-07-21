using server.SRC.Models;

namespace server.SRC.Services
{
    public interface IBlogService
    {
        public Task<Blog> Save (Blog blog);
    }
}