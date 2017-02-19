using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileShare.Core.Files;
using MongoDB.Bson;

namespace FileShare.AmazonS3.Files
{
    public class AmazonFileService : IFileService
    {
        private readonly IFileRepository fileRepo;
        private readonly IMetadataRepository metadataRepo;

        public AmazonFileService()
        {
            this.fileRepo = new AmazonFileRepository();
            this.metadataRepo = new MetadataRepository();
        }

        public async Task SaveFiles(IEnumerable<FileDto> files)
        {
            var tasks = files.Select(SaveFile).ToList();

            await Task.WhenAll(tasks);
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

        public async Task SaveFile(FileDto file)
        {
            var fileTask = fileRepo.SaveFile(file);
            var metadataTask = metadataRepo.SaveMetadata(file.Metadata);

            await Task.WhenAll(fileTask, metadataTask);
        }
    }
}