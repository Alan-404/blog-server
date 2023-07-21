using server.SRC.Models;
using server.SRC.Configs;
using server.SRC.Utils;
using System.Data;
using Microsoft.AspNetCore.Identity;
namespace server.SRC.Services.Providers
{
    public class AccountProvider: IAccountService
    {
        private readonly DatabaseContext _context;
        private readonly PasswordHasher<string> _hasher;

        public AccountProvider(DatabaseContext context)
        {
            this._context = context;
            this._hasher = new PasswordHasher<string>();
        }

        public async Task<Account> Save(Account account)
        {
            try
            {
                account.Id = Library.GenerateId(Constant.lengthId);
                account.Password = this._hasher.HashPassword(null, account.Password);
                account.ModifiedAt = DateTime.Now;
                await this._context.AddAsync(account);
                await this._context.SaveChangesAsync();
                return account;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}