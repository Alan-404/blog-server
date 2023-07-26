using server.SRC.DB;
using server.SRC.Models;
using Microsoft.EntityFrameworkCore;


namespace server.SRC.Services.Providers
{
    public class SocialNetworkProvider: ISocialNetworkService
    {
        private readonly DatabaseContext _context;
        private readonly string _storagePath = "./Storage/network";
        public SocialNetworkProvider(DatabaseContext context)
        {
            this._context = context;
        }

        public async Task<SocialNetwork> Save(SocialNetwork network)
        {
            try
            {
                await this._context.AddAsync(network);
                await this._context.SaveChangesAsync();
                return network;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<List<SocialNetwork>> GetAll()
        {
            try
            {
                return await this._context.SocialNetworks.ToListAsync();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return new List<SocialNetwork>();
            }
        }

        public async Task<SocialNetwork> GetById (int networkId)
        {
            try
            {
                return await this._context.SocialNetworks.FindAsync(networkId);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<bool> SaveImage (int networkId ,IFormFile file)
        {
            try
            {
                string path = Path.Combine(this._storagePath, networkId.ToString() + ".png");
                using var fileStream = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(fileStream);
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public string GetFilePath (int id)
        {
            string path = Path.Combine(this._storagePath, id.ToString() + ".png");
            if (Path.Exists(path) == false) return null;

            return path;
        }
    }
}