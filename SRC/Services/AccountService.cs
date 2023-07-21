using server.SRC.Models;

namespace server.SRC.Services
{
    public interface IAccountService
    {
        public Task<Account> Save (Account account);
        public Task<Account> GetByUserId (string userId);
        public bool CheckPassword (string encoded, string raw);
        public Task<Account> GetById(string id);
    }
}