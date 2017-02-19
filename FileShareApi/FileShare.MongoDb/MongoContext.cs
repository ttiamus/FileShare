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
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("foo");
            AmazonMetadata = database.GetCollection<AmazonMetadata>("AmazonMetadata");
            DirectoryMetadata = database.GetCollection<DirectoryMetadata>("DirectoryMetadata");
        }
    }
}