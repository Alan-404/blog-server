using server.SRC.DB;
using server.SRC.Models;
using server.SRC.Utils;
using Microsoft.EntityFrameworkCore;
namespace server.SRC.Services.Providers
{
    public class UserProvider: IUserService
    {
        private readonly DatabaseContext _context;
        private readonly string _storagePath = "./Storage/user";

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

        public async Task<User> Edit(User user)
        {
            try
            {
                user.ModifiedAt = DateTime.Now;
                this._context.Users.Attach(user);
                this._context.Users.Update(user);
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

        public string GetAvatarPath(string id)
        {
            string path = Path.Combine(this._storagePath, id + ".png");
            if (Path.Exists(path)) return path;
            return null;
        }

        public async Task<List<User>> GetAll()
        {
            try
            {
                return await this._context.Users.ToListAsync();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return new List<User>();
            }
        }

        public bool DeleteAvatar(string id)
        {
            try
            {
                string path = Path.Combine(this._storagePath, id + ".png");
                if (Path.Exists(path))
                    return true;
                File.Delete(path);
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<bool> SaveAvatar(string id, IFormFile avatar)
        {
            try
            {
                string path = Path.Combine(this._storagePath, id + ".png");
                using var fileStream = new FileStream(path, FileMode.Create);
                await avatar.CopyToAsync(fileStream);
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