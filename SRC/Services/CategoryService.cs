using server.SRC.Models;


namespace server.SRC.Services
{
    public interface ICategoryService
    {
        public Task<Category> Save(Category category);
        public Task<Category> GetById(string id);
        public Task<List<Category>> GetAll();
    }
}