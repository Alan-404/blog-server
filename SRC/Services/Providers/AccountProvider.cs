using server.SRC.Models;
using server.SRC.Configs;
using server.SRC.Utils;
using System.Data;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Account> GetByUserId (string userId)
        {
            try
            {
                return await this._context.Accounts.FirstOrDefaultAsync(p => p.UserId == userId);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public bool CheckPassword (string encoded, string raw)
        {
            try
            {
                var passwordVerificationResult = this._hasher.VerifyHashedPassword(null, encoded, raw);
                return (passwordVerificationResult == PasswordVerificationResult.Success);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<Account> GetById(string id)
        {
            try
            {
                return await this._context.Accounts.FindAsync(id);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}