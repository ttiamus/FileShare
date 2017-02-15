using System;
using MongoDB.Bson;

namespace FileShare.MongoDb.Files
{
    public class DirectoryFile
    {
        public ObjectId Id { get; set; }
        public string FileName { get; set; }
        public int Size { get; set; } 
        public string MimeType { get; set; }
        public DateTime DateAdded { get; set; }
        public string UploadedBy { get; set; }
    }
}