using server.SRC.Models;
using server.SRC.DB;
using server.SRC.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace server.SRC.Services.Providers
{
    public class MediaProvider: IMediaService
    {
        private readonly DatabaseContext _context;
        private readonly string _storage = "./Storage/media";

        public MediaProvider(DatabaseContext context)
        {
            this._context = context;
        }

        public async Task<Media> Save(Media media)
        {
            try
            {
                media.Id = Library.GenerateId(20);
                await this._context.Medias.AddAsync(media);
                await this._context.SaveChangesAsync();
                return media;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}