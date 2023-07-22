using server.SRC.Models;
using server.SRC.DB;
using server.SRC.Utils;
using Microsoft.EntityFrameworkCore;
namespace server.SRC.Services.Providers
{
    public class CommentProvider: ICommentService
    {
        private readonly DatabaseContext _context;

        public CommentProvider(DatabaseContext context)
        {
            this._context = context;
        }


        public async Task<Comment> Save(Comment comment)
        {
            try
            {
                comment.Id = Library.GenerateId(20);
                comment.CreatedAt = DateTime.Now;
                comment.ModifiedAt = DateTime.Now;
                await this._context.Comments.AddAsync(comment);
                await this._context.SaveChangesAsync();
                return comment;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<List<Comment>> GetAllRootByBlogId(string blogId)
        {
            try
            {
                return await this._context.Comments.Where(p => p.BlogId == blogId && p.Reply == null).ToListAsync();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return new List<Comment>();
            }
        }

        public async Task<List<Comment>> PaginateRootByBlogId(string blogId, int page, int num)
        {
            try
            {
                return await this._context.Comments.Where(p => p.BlogId == blogId && p.Reply == null).Skip((page-1)*num).Take(num).ToListAsync();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return new List<Comment>();
            }
        }

        public async Task<List<Comment>> GetAllByBlogId (string blogId)
        {
            try
            {
                return await this._context.Comments.Where(p => p.BlogId == blogId).ToListAsync();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return new List<Comment>();
            }
        }
    }
}