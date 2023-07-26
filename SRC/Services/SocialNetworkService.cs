using server.SRC.Models;

namespace server.SRC.Services
{
    public interface ISocialNetworkService
    {
        public Task<SocialNetwork> Save (SocialNetwork network);
        public Task<SocialNetwork> GetById (int id);
        public Task<List<SocialNetwork>> GetAll();
        public Task<bool> SaveImage (int id ,IFormFile file);
        public string GetFilePath (int id);
    }
}