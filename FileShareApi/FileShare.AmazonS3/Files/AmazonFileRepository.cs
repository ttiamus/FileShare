using System;
using System.IO;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Transfer;
using FileShare.Core.Files;

namespace FileShare.AmazonS3.Files
{
    public class AmazonFileRepository : IFileRepository
    {
        static string bucket = "ht-file-share";

        public async Task SaveFile(FileDto file)
        {
            try
            {
                TransferUtility fileTransferUtility = new
                    TransferUtility(new AmazonS3Client(Amazon.RegionEndpoint.USEast1));
                
                using (MemoryStream fileToUpload = new MemoryStream(file.FileBytes))
                {
                    var request = new TransferUtilityUploadRequest()
                    {
                        AutoCloseStream = true,
                        BucketName = bucket,
                        ContentType = file.Metadata.MimeType,
                        InputStream = fileToUpload,
                        StorageClass = S3StorageClass.ReducedRedundancy,
                        PartSize = 6291456, // 6 MB.
                        Key = file.Metadata.FileName,
                        CannedACL = S3CannedACL.PublicRead
                    };
                    await fileTransferUtility.UploadAsync(request);
                }
                
            }
            catch (AmazonS3Exception s3Exception)
            {
                Console.WriteLine(s3Exception.Message,
                                  s3Exception.InnerException);
            }
        }
    

        public Task<FileDto> GetFile(IMetadata metadata)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteFile(IMetadata metadata)
        {
            throw new System.NotImplementedException();
        }
    }
}