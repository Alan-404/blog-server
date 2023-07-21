using server.SRC.Models;
using server.SRC.Configs;
using server.SRC.Utils;
using Microsoft.EntityFrameworkCore;

namespace server.SRC.Services.Providers
{
    public class BlogProvider: IBlogService
    {
        private readonly DatabaseContext _context;
        private readonly string _storagePath = "./Storage/blog";

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

        public async Task<bool> SaveThumnail(IFormFile thumnail, string id)
        {
            try
            {
                string filePath = Path.Combine(this._storagePath, id + ".png");
                using var fileStream = new FileStream(filePath, FileMode.Create);
                await thumnail.CopyToAsync(fileStream);
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return false;
            } 
        }

        public string GetThumnailLink(string id)
        {
            return Path.Combine(this._storagePath, id + ".png");
        }
    }
}