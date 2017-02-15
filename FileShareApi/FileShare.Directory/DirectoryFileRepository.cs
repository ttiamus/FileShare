using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using FileShare.Core.Files;
using FileShare.MongoDb;
using FileShare.MongoDb.Files;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FileShare.Directory
{
    public class DirectoryFileRepository : IFileRepository
    {
        private readonly DirectoryConfiguration config;
        private readonly MongoContext context;
        DirectoryFileRepository()
        {
            this.config = new DirectoryConfiguration();
            this.context = new MongoContext();
        }

        public async Task SaveFile(FileDto file)
        {
            var task = Task.Run(async () =>
            {
                await SaveFileToFileSystem(file);
                await SaveMetadataToDatabase(file); //I think if I take await off this will run aync a little better
            });

            await task;
        }

        private async Task SaveFileToFileSystem(FileDto file)
        {
            var task = Task.Run(async () =>
            {
                var fileStream =
                    new FileStream($@"{config.FileSaveLocation}\{file.Name}", FileMode.Create,
                    FileAccess.Write, System.IO.FileShare.None, bufferSize: 4096, useAsync: true);

                await fileStream.WriteAsync(file.FileBytes, 0, file.FileBytes.Length);

                fileStream.Dispose();
            });

            await task;
        }

        private async Task SaveMetadataToDatabase(FileDto file)
        {
            var task = Task.Run(() =>
            {
                var directoryFile = new DirectoryFile()
                {
                    Id = file.Id,
                    FileName = file.Name,
                    MimeType = file.MimeType,
                    Size = file.FileBytes.Length,
                    UploadedBy = "CurrentUser",
                    DateAdded = DateTime.Now.Date
                };
                context.DirectoryFiles.InsertOneAsync(directoryFile);
            });

            await task;
        }
    }
}