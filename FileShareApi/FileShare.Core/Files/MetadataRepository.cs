using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileShare.MongoDb;
using FileShare.MongoDb.Files;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FileShare.Core.Files
{
    public class MetadataRepository : IMetadataRepository
    {
        private readonly MongoContext context;

        public MetadataRepository()
        {
            this.context = new MongoContext();
        }

        public async Task SaveMetadata(IMetadata file)
        {
            var task = Task.Run(() =>
            {
                var directoryFile = new DirectoryMetadata()
                {
                    Id = file.Id,
                    FileName = file.FileName,
                    MimeType = file.MimeType,
                    Size = file.FileSize,
                    UploadedBy = "CurrentUser",
                    DateAdded = DateTime.Now.Date
                };
                context.DirectoryMetadata.InsertOneAsync(directoryFile);
            });

            await task;
        }

        public async Task<IEnumerable<Metadata>> GetMetadataForAllFiles()
        {
            var result = await context.DirectoryMetadata.Find(x => true).ToListAsync();
            return result.Select(x => new Metadata(x.Id, x.FileName, x.MimeType, x.Size));
        }

        public async Task<Metadata> GetMetadataForFile(ObjectId id)
        {
            var result = await context.DirectoryMetadata.Find(x => x.Id == id).ToListAsync();
            var metadata = result.First();
            return new Metadata(metadata.Id, metadata.FileName, metadata.MimeType, metadata.Size);
        }

        public async Task DeleteMetadata(ObjectId id)
        {
            await context.DirectoryMetadata.DeleteOneAsync(x => x.Id.Equals(id));
        }
    }
}
