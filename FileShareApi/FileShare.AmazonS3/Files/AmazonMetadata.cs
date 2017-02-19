using FileShare.Core.Files;
using MongoDB.Bson;

namespace FileShare.AmazonS3.Files
{
    public class AmazonMetadata : Metadata, IMetadata
    {
        public string FileKey { get; set; }

        public AmazonMetadata(string name, string mimeType, int fileSize) : base(name, mimeType, fileSize) {}
        public AmazonMetadata(ObjectId id, string name, string mimeType, int fileSize) : base(id, name, mimeType, fileSize) {}

        
    }
}