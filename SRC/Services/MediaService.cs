using server.SRC.Models;
namespace server.SRC.Services
{
    public interface IMediaService
    {
        public Task<Media> Save(Media media);
    }
}