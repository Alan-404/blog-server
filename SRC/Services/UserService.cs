using server.SRC.Models;

namespace server.SRC.Services
{
    public interface IUserService
    {
        public Task<User> Save(User user);
    }
}