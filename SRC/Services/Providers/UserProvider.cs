using server.SRC.DB;
using server.SRC.Models;
using server.SRC.Utils;
using Microsoft.EntityFrameworkCore;
namespace server.SRC.Services.Providers
{
    public class UserProvider: IUserService
    {
        private readonly DatabaseContext _context;

        public UserProvider(DatabaseContext context)
        {
            this._context = context;
        }

        public async Task<User> Save(User user)
        {
            try
            {
                user.Id = Library.GenerateId(Constant.lengthId);
                user.CreatedAt = DateTime.Now;
                user.ModifiedAt = DateTime.Now;
                await this._context.Users.AddAsync(user);
                await this._context.SaveChangesAsync();
                return user;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<User> GetByEmail (string email)
        {
            try
            {
                return await this._context.Users.FirstOrDefaultAsync(p => p.Email == email);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<User> GetById (string id)
        {
            try
            {
                return await this._context.Users.FindAsync(id);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        
    }
}