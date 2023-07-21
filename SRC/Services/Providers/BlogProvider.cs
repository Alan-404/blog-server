using server.SRC.Models;
using server.SRC.Configs;
using server.SRC.Utils;

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
    }
}