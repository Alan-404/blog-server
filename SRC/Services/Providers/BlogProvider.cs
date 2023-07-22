using server.SRC.Models;
using server.SRC.DB;
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
                blog.NumViews = 0;
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

        public async Task<Blog> Edit (Blog blog)
        {
            try
            {
                this._context.Blogs.Attach(blog);
                this._context.Blogs.Update(blog);
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

        public async Task<List<Blog>> Paginate(int page, int num)
        {
            return await this._context.Blogs.Skip((page-1)*num).Take(num).ToListAsync();
        }

        public string GetThumnailLink(string id)
        {
            string path =  Path.Combine(this._storagePath, id + ".png");
            if (Path.Exists(path)) return path;
            return null;
        }
    }
}