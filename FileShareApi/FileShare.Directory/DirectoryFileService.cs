using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileUpload.Core.Files;

namespace FileUpload.Directory
{
    public class DirectoryFileService : IFileService
    {
        public async Task<IEnumerable<FileDto>> GetAvailableFileNames()
        {
            //Get these from Database instead of File System
            var config = new DirectoryConfiguration();
            var files = Task.Run(() => 
                System.IO.Directory.GetFiles(config.FileSaveLocation)
                .Select(file => new FileDto(Path.GetFileName(file))).ToList());
            return await files;
        }

        public async Task SaveFile(FileDto file)
        {
            var config = new DirectoryConfiguration();

            var fileStream =  
                new FileStream($@"{config.FileSaveLocation}\{file.Name}", FileMode.Create, 
                FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true);

            await fileStream.WriteAsync(file.FileBytes, 0, file.FileBytes.Length);

            //File.WriteAllBytes($@"{config.FileSaveLocation}\{file.Name}", file.FileBytes);
            fileStream.Dispose();
        }

        //Returns the file path
        public async Task<string> GetFile(string key)
        {
            //Get fileName by key from database....
            var fileName = "pdf.pdf";
            var config = new DirectoryConfiguration();
            var files = Task.Run(() => 
                System.IO.Directory.GetFiles(config.FileSaveLocation)
                .ToList().First(file => 
                    Path.GetFileName(file).Equals(fileName, StringComparison.InvariantCultureIgnoreCase)
                )
            );

            //Use key to filter instead....
            return await files;
        }

        public Task DeleteFile(string key)
        {
            //use key to get fileName...
            var fileName = "test.txt";
            var config = new DirectoryConfiguration();
            var path = $@"{config.FileSaveLocation}\{fileName}";
            return Task.Run(() => { File.Delete(path); });
        }
    }
}
