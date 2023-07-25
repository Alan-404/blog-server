using server.SRC.Models;

namespace server.SRC.Services
{
    public interface IBlogCategoryService
    {
        public Task<BlogCategory> Save(BlogCategory item);
        public Task<bool> Remove(BlogCategory item);
        public Task<BlogCategory> GetByBlogIdAndCategoryId (string blogId, string categoryId);
        public Task<List<BlogCategory>> GetByBlogId(string blogId);
    }
}