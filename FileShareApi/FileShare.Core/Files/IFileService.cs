using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileShare.Core.Files
{
    public interface IFileService
    {
        Task<IEnumerable<FileDto>> GetAvailableFileNames();
        Task<string> GetFile(string key);
        Task DeleteFile(string key);
        Task SaveFile(FileDto file);
    }
}