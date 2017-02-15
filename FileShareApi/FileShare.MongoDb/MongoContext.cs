using FileShare.MongoDb.Files;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FileShare.MongoDb
{
    public class MongoContext
    {
        public IMongoCollection<AmazonFile> AmazonFiles { get; set; }
        public IMongoCollection<DirectoryFile> DirectoryFiles { get; set; }

        public MongoContext()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("foo");
            AmazonFiles = database.GetCollection<AmazonFile>("AmazonFiles");
            DirectoryFiles = database.GetCollection<DirectoryFile>("DirectoryFiles");
        }
    }
}