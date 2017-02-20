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

        public async Task<IEnumerable<IMetadata>> GetFileNames()
        {
            return await metadataRepo.GetMetadataForAllFiles();
        }

        public async Task<FileDto> GetFile(ObjectId key)
        {
            var metadataTask = metadataRepo.GetMetadataForFile(key);
            return await fileRepo.GetFile(await metadataTask);
        }

        public async Task SaveFile(FileDto file)
        {
            var fileTask = fileRepo.SaveFile(file);
            var metadataTask = metadataRepo.SaveMetadata(file.Metadata);

            await Task.WhenAll(fileTask, metadataTask);
        }

        public async Task SaveFiles(IEnumerable<FileDto> files)
        {
            var tasks = files.Select(SaveFile).ToList();

            await Task.WhenAll(tasks);
        }

        public async Task UpdateFile(FileDto file)
        {
            var fileTask = fileRepo.SaveFile(file);     //If the file has the same key, then it will be updated by default
            var metadataTask = metadataRepo.SaveMetadata(file.Metadata, true);

            await Task.WhenAll(fileTask, metadataTask);
        }

        public async Task DeleteFile(ObjectId key)
        {
            var metadata = await metadataRepo.GetMetadataForFile(key);
            var deleteFileTask = fileRepo.DeleteFile(metadata);
            var deleteMetadataTask = metadataRepo.DeleteMetadata(key);

            await Task.WhenAll(deleteFileTask, deleteMetadataTask);
        }
    }
}