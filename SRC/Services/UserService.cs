using server.SRC.Models;

namespace server.SRC.Services
{
    public interface IUserService
    {
        public Task<User> Save(User user);
        public Task<User> GetById (string id);
        public Task<User> GetByEmail (string email);
    }
}