using System.Collections.Generic;
using System.Threading.Tasks;
using FileShare.Core.Files;
using MongoDB.Bson;

namespace FileShare.Directory
{
    public interface IMetadataRepository
    {
        Task SaveMetadata(Metadata file);
        Task<IEnumerable<Metadata>> GetMetadataForAllFiles();
        Task<Metadata> GetMetadataForFile(ObjectId id);
        Task DeleteMetadata(ObjectId id);
    }
}