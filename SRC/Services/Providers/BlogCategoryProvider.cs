using Microsoft.EntityFrameworkCore;
using server.SRC.DB;
using server.SRC.Models;

namespace server.SRC.Services.Providers
{
    public class BlogCategoryProvider: IBlogCategoryService
    {
        private readonly DatabaseContext _context;

        public BlogCategoryProvider(DatabaseContext context)
        {
            this._context = context;
        }

        public async Task<BlogCategory> Save(BlogCategory item)
        {
            try
            {
                await this._context.AddAsync(item);
                await this._context.SaveChangesAsync();
                return item;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<BlogCategory> GetByBlogIdAndCategoryId (string blogId, string categoryId)
        {
            try
            {
                return await this._context.BlogCategories.FirstOrDefaultAsync(p => p.BlogId == blogId && p.CategoryId == categoryId);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<List<BlogCategory>> GetByBlogId(string blogId)
        {
            try
            {
                return await this._context.BlogCategories.Where(p => p.BlogId == blogId).ToListAsync();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return new List<BlogCategory>();
            }
        }

        public async Task<bool> Remove(BlogCategory item)
        {
            try 
            {
                this._context.Remove(item);
                await this._context.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<BlogCategory> GetFirstByBlogId(string blogId)
        {
            try
            {
                return await this._context.BlogCategories.FirstOrDefaultAsync(p => p.BlogId == blogId);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}