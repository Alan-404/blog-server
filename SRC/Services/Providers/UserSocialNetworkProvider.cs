using Microsoft.EntityFrameworkCore;
using server.SRC.DB;
using server.SRC.Models;


namespace server.SRC.Services.Providers
{
    public class UserSocialNetworkProvider: IUserSocialNetworkService
    {
        private readonly DatabaseContext _context;
        public UserSocialNetworkProvider(DatabaseContext context)
        {
            this._context = context;
        }

        public async Task<UserSocialNetwork> Save(UserSocialNetwork item)
        {
            try
            {
                await this._context.AddAsync(item);
                await this._context.SaveChangesAsync();
                return item;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<UserSocialNetwork> GetById (int id)
        {
            try
            {
                return await this._context.UserSocialNetworks.FindAsync(id);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<List<UserSocialNetwork>> GetByUserId (string userId)
        {
            try
            {
                return await this._context.UserSocialNetworks.Where(p => p.UserId == userId).ToListAsync();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return new List<UserSocialNetwork>();
            }
        }

        public async Task<UserSocialNetwork> GetByUserIdAndNetworkId (string userId, int networkId)
        {
            try
            {
                return await this._context.UserSocialNetworks.Where(p => p.UserId == userId && p.NetworkId == networkId).FirstOrDefaultAsync();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<bool> Remove (UserSocialNetwork item)
        {
            try
            {
                this._context.UserSocialNetworks.Remove(item);
                await this._context.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}