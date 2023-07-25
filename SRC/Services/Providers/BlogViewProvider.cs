using server.SRC.Models;
using server.SRC.DB;
using Microsoft.EntityFrameworkCore;
namespace server.SRC.Services.Providers
{
    public class BlogViewProvider: IBlogViewSerivce
    {
        private readonly DatabaseContext _context;


        public BlogViewProvider(DatabaseContext context)
        {
            this._context = context;
        }

        public async Task<BlogView> GetByUserIdAndBlogId(string userId, string blogId)
        {
            try
            {
                return await this._context.BlogViews.FirstOrDefaultAsync(p => p.UserId == userId && p.BlogId == blogId);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<BlogView> Edit(BlogView item)
        {
            try
            {
                this._context.BlogViews.Attach(item);
                this._context.BlogViews.Update(item);
                await this._context.SaveChangesAsync();
                return item;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<List<BlogView>> GetByBlogId(string blogId)
        {
            try
            {
                return await this._context.BlogViews.Where(p => p.BlogId == blogId).ToListAsync();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return new List<BlogView>();
            }
        }

        public async Task<int> GetNumViewsByBlogId (string blogId)
        {
            try
            {
                return (await this.GetByBlogId(blogId)).Sum(p => p.Num);

            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }
        }

        public async Task<BlogView> Save(string userId, string blogId)
        {
            try
            {
                BlogView item = await this.GetByUserIdAndBlogId(userId, blogId);
                if (item != null)
                {
                    item.Num++;
                    BlogView savedItem = await this.Edit(item);
                    return savedItem;
                }
                BlogView newItem = new BlogView(userId, blogId, 1);
                await this._context.BlogViews.AddAsync(newItem);
                await this._context.SaveChangesAsync();
                return newItem;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}