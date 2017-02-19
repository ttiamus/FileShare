using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using FileShare.Core.Files;
using FileShare.MongoDb;
using FileShare.MongoDb.Files;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.Core.WireProtocol.Messages.Encoders;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;

namespace FileShare.Directory
{
    public class DirectoryFileRepository : IFileRepository
    {
        private readonly DirectoryConfiguration config;
        
        public DirectoryFileRepository()
        {
            this.config = new DirectoryConfiguration();
        }

        public async Task SaveFile(FileDto file)
        {
            var task = Task.Run(async () =>
            {
                var fileStream =
                    new FileStream($@"{config.FileSaveLocation}\{file.Metadata.FileName}", FileMode.Create,
                    FileAccess.Write, System.IO.FileShare.None, bufferSize: 4096, useAsync: true);

                await fileStream.WriteAsync(file.FileBytes, 0, file.FileBytes.Length);

                fileStream.Dispose();
            });

            await task;
        }

        public async Task<FileDto> GetFile(IMetadata metadata)
        {
            var fileTask = Task.Run<byte[]>( () =>
                File.ReadAllBytes($@"{config.FileSaveLocation}\{metadata.FileName}")
            
            //System.IO.Directory.GetFiles(config.FileSaveLocation)
                //.ToList().First(file => Path.GetFileName(file).Equals(metadata.FileName, StringComparison.InvariantCultureIgnoreCase))
            );

            return new FileDto(metadata, await fileTask);
        }

        public async Task DeleteFile(IMetadata metadata)
        {
            var path = $@"{config.FileSaveLocation}\{metadata.FileName}";
            await Task.Run(() => { File.Delete(path); });
        }  
    }
}