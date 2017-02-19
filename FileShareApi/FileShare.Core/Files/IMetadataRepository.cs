using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace FileShare.Core.Files
{
    public interface IMetadataRepository
    {
        Task SaveMetadata(IMetadata file);
        Task<IEnumerable<Metadata>> GetMetadataForAllFiles();
        Task<Metadata> GetMetadataForFile(ObjectId id);
        Task DeleteMetadata(ObjectId id);
    }
}