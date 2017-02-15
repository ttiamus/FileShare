using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FileShare.Core.Files
{
    public interface IFileRepository
    {
        Task SaveFile(FileDto file);
    }
}