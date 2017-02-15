using System;
using MongoDB.Bson;

namespace FileShare.Core.Files
{
    public class FileDto
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string MimeType { get; set; }
        public byte[] FileBytes { get; set; }

        public FileDto(string name)
        {
            this.Id = ObjectId.GenerateNewId();
            this.Name = name;
        }

        public FileDto(string name, byte[] fileBytes, string mimeType)
        {
            this.Id = ObjectId.GenerateNewId();
            this.Name = name;
            this.FileBytes = fileBytes;
            this.MimeType = mimeType;
        }
    }
}