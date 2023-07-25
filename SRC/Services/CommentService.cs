using server.SRC.Models;

namespace server.SRC.Services
{
    public interface ICommentService
    {
        public Task<Comment> Save(Comment comment);
        public Task<Comment> GetById(string id);
        public Task<bool> Remove(Comment comment);
        public Task<List<Comment>> GetAllRootByBlogId(string blogId);
        public Task<List<Comment>> GetAllByBlogId (string blogId);
        public Task<List<Comment>> PaginateRootByBlogId(string blogId, int page, int num);
        public Task<List<Comment>> GetAllRepliesById (string id);
        public Task<List<Comment>> PaginateRepliesById (string id, int page, int num);
    }
}