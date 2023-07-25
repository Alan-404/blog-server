using server.SRC.DB;
using server.SRC.Models;
using Microsoft.EntityFrameworkCore;
namespace server.SRC.Services.Providers
{
    public class CommentLikeProvider: ICommentLikeService
    {
        private readonly DatabaseContext _context;

        public CommentLikeProvider(DatabaseContext context)
        {
            this._context = context;
        }

        public async Task<CommentLike> GetByCommentIdAndUserId (string commentId, string userId)
        {
            try
            {
                return await this._context.CommentLikes.FirstOrDefaultAsync(p => p.CommentId == commentId && p.UserId == userId);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<CommentLike> Save(CommentLike like)
        {
            try
            {
                await this._context.AddAsync(like);
                await this._context.SaveChangesAsync();
                return like;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<List<CommentLike>> GetByCommentId (string commentId)
        {
            try
            {
                return await this._context.CommentLikes.Where(p => p.CommentId == commentId).ToListAsync();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return new List<CommentLike>();
            }
        }

        public async Task<bool> Remove(CommentLike like)
        {
            try
            {
                this._context.Remove(like);
                await this._context.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}