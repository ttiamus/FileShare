using FileShare.MongoDb.Files;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FileShare.MongoDb
{
    public class MongoContext
    {
        public IMongoCollection<AmazonMetadata> AmazonMetadata { get; set; }
        public IMongoCollection<DirectoryMetadata> DirectoryMetadata{ get; set; }

        public MongoContext()
        {
            var client = new MongoClient(new MongoConfiguration().ConnectionString);
            var database = client.GetDatabase("fileshare");
            AmazonMetadata = database.GetCollection<AmazonMetadata>("AmazonMetadata");
            DirectoryMetadata = database.GetCollection<DirectoryMetadata>("DirectoryMetadata");
        }
    }
}