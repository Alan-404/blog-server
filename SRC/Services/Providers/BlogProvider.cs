using server.SRC.Models;
using server.SRC.Configs;
using server.SRC.Utils;
using Microsoft.EntityFrameworkCore;

namespace server.SRC.Services.Providers
{
    public class BlogProvider: IBlogService
    {
        private readonly DatabaseContext _context;

        public BlogProvider(DatabaseContext context)
        {
            this._context = context;
        }

        public async Task<Blog> Save(Blog blog)
        {
            try
            {
                blog.Id = Library.GenerateId(Constant.lengthId);
                blog.CreatedAt = DateTime.Now;
                blog.ModifiedAt = DateTime.Now;
                await this._context.AddAsync(blog);
                await this._context.SaveChangesAsync();
                return blog;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<List<Blog>> GetAll()
        {
            try
            {
                return await this._context.Blogs.OrderByDescending(p => p.CreatedAt).ToListAsync();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return new List<Blog>();
            }
        }

        public async Task<Blog> GetById (string id)
        {
            try
            {
                return await this._context.Blogs.FindAsync(id);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}