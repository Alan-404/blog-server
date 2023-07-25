using server.SRC.DB;
using server.SRC.Models;
using server.SRC.Utils;
using Microsoft.EntityFrameworkCore;


namespace server.SRC.Services.Providers
{
    public class CategoryProvider: ICategoryService
    {
        private readonly DatabaseContext _context;

        public CategoryProvider(DatabaseContext context)
        {
            this._context = context;
        }

        public async Task<Category> Save(Category category)
        {
            try
            {
                category.Id = Library.GenerateId(Constant.lengthId);
                await this._context.Categories.AddAsync(category);
                await this._context.SaveChangesAsync();
                return category;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<Category> GetById(string id)
        {
            try
            {
                return await this._context.Categories.FindAsync(id);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}