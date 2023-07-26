using server.SRC.Models;


namespace server.SRC.Services
{
    public interface IUserSocialNetworkService
    {
        public Task<UserSocialNetwork> Save(UserSocialNetwork item);
        public Task<UserSocialNetwork> GetById (int id);
        public Task<List<UserSocialNetwork>> GetByUserId (string userId);
        public Task<UserSocialNetwork> GetByUserIdAndNetworkId (string userId, int networkId);
        public Task<bool> Remove (UserSocialNetwork item);
    }
}