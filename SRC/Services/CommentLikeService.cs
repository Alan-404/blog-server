using server.SRC.Models;

namespace server.SRC.Services
{
    public interface ICommentLikeService
    {
        public Task<CommentLike> Save(CommentLike like);
        public Task<bool> Remove(CommentLike like);
        public Task<CommentLike> GetByCommentIdAndUserId (string commentId, string userId);
        public Task<List<CommentLike>> GetByCommentId (string commentId);
    }
}