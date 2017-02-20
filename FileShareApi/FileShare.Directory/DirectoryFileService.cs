using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileShare.Core.Files;
using MongoDB.Bson;

namespace FileShare.Directory
{
    public class DirectoryFileService : IFileService
    {
        private readonly IFileRepository fileRepo;
        private readonly IMetadataRepository metadataRepo;

        public DirectoryFileService()
        {
            this.fileRepo = new DirectoryFileRepository();
            this.metadataRepo = new MetadataRepository();
        }

        public async Task<IEnumerable<IMetadata>> GetFileNames()
        {
            return await metadataRepo.GetMetadataForAllFiles();
        }

        public async Task SaveFiles(IEnumerable<FileDto> files)
        {
            var saveTasks = files.Select(SaveFile).ToList();
            await Task.WhenAll(saveTasks);
        }

        public async Task SaveFile(FileDto file)
        {

            var fileTask = fileRepo.SaveFile(file);
            var metadataTask = metadataRepo.SaveMetadata(file.Metadata);

            try
            {
                await Task.WhenAll(fileTask, metadataTask);
            }
            catch(Exception e)
            {
                //Log exception

                if (fileTask.IsFaulted)
                {
                    //rollBack the fileSave
                }

                if (metadataTask.IsFaulted)
                {
                    //rollback metadata save
                }
            }
        }

        //Returns the file path
        public async Task<FileDto> GetFile(ObjectId key)
        {
            var metadata = await metadataRepo.GetMetadataForFile(key);
            return await fileRepo.GetFile(metadata);
        }

        public async Task DeleteFile(ObjectId id)
        {
            var metadata = await metadataRepo.GetMetadataForFile(id);
            var deleteFileTask = fileRepo.DeleteFile(metadata);
            var deleteMetadataTask = metadataRepo.DeleteMetadata(id);

            await Task.WhenAll(deleteFileTask, deleteMetadataTask);
        }
    }
}
