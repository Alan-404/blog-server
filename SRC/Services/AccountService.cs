using server.SRC.Models;

namespace server.SRC.Services
{
    public interface IAccountService
    {
        public Task<Account> Save (Account account);
    }
}