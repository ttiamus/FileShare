using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace FileShare.Core.Files
{
    public interface IFileService
    {
        Task SaveFiles(IEnumerable<FileDto> files);
        Task<IEnumerable<IMetadata>> GetAvailableFileNames();
        Task<FileDto> GetFile(ObjectId key);
        Task DeleteFile(ObjectId key);
        Task SaveFile(FileDto file);
    }
}