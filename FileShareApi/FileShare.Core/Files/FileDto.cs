using System;
using MongoDB.Bson;

namespace FileShare.Core.Files
{
    public class FileDto
    {
        
        public byte[] FileBytes { get; set; }
        public IMetadata Metadata { get; set; }

        public FileDto(IMetadata metadata, byte[] fileBytes)
        {
            this.Metadata = metadata;
            this.FileBytes = fileBytes;
        }

        public FileDto(string name, byte[] fileBytes, string mimeType)
        {
            this.FileBytes = fileBytes;
            this.Metadata = new Metadata(name, mimeType, fileBytes.Length);
        }

        public FileDto(string id, string name, byte[] fileBytes, string mimeType)
        {
            this.FileBytes = fileBytes;
            this.Metadata = new Metadata(ObjectId.Parse(id), name, mimeType, fileBytes.Length);
        }
    }

    public interface IMetadata
    {
        ObjectId Id { get; set; }
        string FileName { get; set; }
        string MimeType { get; set; }
        int FileSize { get; set; }
    }

    public class Metadata : IMetadata
    {
        public ObjectId Id { get; set; }
        public string FileName { get; set; }
        public string MimeType { get; set; }
        public int FileSize { get; set; }

        public Metadata(string name, string mimeType, int fileSize)
        {
            //this.Id = ObjectId.GenerateNewId();
            FileName = name;
            MimeType = mimeType;
            FileSize = fileSize;
        }

        public Metadata(ObjectId id, string name, string mimeType, int fileSize)
        {
            this.Id = id;
            FileName = name;
            MimeType = mimeType;
            FileSize = fileSize;
        }
    }
}