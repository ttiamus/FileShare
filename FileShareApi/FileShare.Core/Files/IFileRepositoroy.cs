using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace FileShare.Core.Files
{
    public interface IFileRepository
    {
        Task SaveFile(FileDto file);
        Task<FileDto> GetFile(IMetadata metadata);
        Task DeleteFile(IMetadata metadata);
    }
}