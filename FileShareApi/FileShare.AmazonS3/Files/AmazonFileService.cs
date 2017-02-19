using System.Collections.Generic;
using System.Threading.Tasks;
using FileShare.Core.Files;
using MongoDB.Bson;

namespace FileShare.AmazonS3.Files
{
    public class AmazonFileService : IFileService
    {
        public Task SaveFiles(IEnumerable<FileDto> files)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<IMetadata>> GetAvailableFileNames()
        {
            throw new System.NotImplementedException();
        }

        public Task<FileDto> GetFile(ObjectId key)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteFile(ObjectId key)
        {
            throw new System.NotImplementedException();
        }

        public Task SaveFile(FileDto file)
        {
            throw new System.NotImplementedException();
        }
    }
}