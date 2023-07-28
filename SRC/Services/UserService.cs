using server.SRC.Models;

namespace server.SRC.Services
{
    public interface IUserService
    {
        public Task<User> Save(User user);
        public Task<User> Edit(User user);
        public Task<User> GetById (string id);
        public Task<User> GetByEmail (string email);
        public string GetAvatarPath(string id);
        public Task<bool> SaveAvatar(string id, IFormFile avatar);
        public bool DeleteAvatar(string id);
        public Task<List<User>> GetAll();
    }
}