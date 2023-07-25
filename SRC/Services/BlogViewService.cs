using server.SRC.Models;
namespace server.SRC.Services
{
    public interface IBlogViewSerivce
    {
        public Task<BlogView> Save(string userId, string blogId);
        public Task<BlogView> Edit(BlogView item);
        public Task<BlogView> GetByUserIdAndBlogId(string userId, string blogId);
        public Task<List<BlogView>> GetByBlogId(string blogId);
        public Task<int> GetNumViewsByBlogId (string blogId);
    }
}