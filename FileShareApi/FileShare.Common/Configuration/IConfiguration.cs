using System.IO;

namespace FileUpload.Common.Configuration
{
    public interface IConfiguration
    {
        string FileSaveLocation { get; }
    }
}