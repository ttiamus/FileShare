using System;

namespace FileUpload.Core.Files
{
    public class FileDto
    {
        public Guid Key { get; set; }
        public string Name { get; set; }
        public byte[] FileBytes { get; set; }

        public FileDto(string name)
        {
            this.Key = Guid.NewGuid();
            this.Name = name;
        }

        public FileDto(string name, byte[] fileBytes)
        {
            this.Key = Guid.NewGuid();
            this.Name = name;
            this.FileBytes = fileBytes;
        }
    }
}