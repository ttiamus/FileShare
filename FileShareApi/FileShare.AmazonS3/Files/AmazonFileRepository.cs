using System.Threading.Tasks;
using FileShare.Core.Files;

namespace FileShare.AmazonS3.Files
{
    public class AmazonFileRepository : IFileRepository
    {
        public Task SaveFile(FileDto file)
        {
            throw new System.NotImplementedException();
        }

        public Task<FileDto> GetFile(IMetadata metadata)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteFile(IMetadata metadata)
        {
            throw new System.NotImplementedException();
        }
    }
}