using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace FileShare.Core.Files
{
    public interface IFileService
    {
        Task SaveFiles(IEnumerable<FileDto> files);
        Task<IEnumerable<IMetadata>> GetFileNames();
        Task<FileDto> GetFile(ObjectId key);
        Task DeleteFile(ObjectId key);
        Task SaveFile(FileDto file);
    }
}