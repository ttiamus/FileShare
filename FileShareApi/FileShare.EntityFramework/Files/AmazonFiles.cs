using System;

namespace FileShare.EntityFramework.Files
{
    public class AmazonFiles
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public int Size { get; set; }
        public string MimeType { get; set; }
        public DateTime DateAdded { get; set; }
        public string UploadedBy { get; set; }
    }
}