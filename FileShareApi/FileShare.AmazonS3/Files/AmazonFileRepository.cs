using System;
using System.IO;
using System.Threading.Tasks;
using Amazon.S3;
﻿using Amazon.S3.Model;
﻿using Amazon.S3.Transfer;
using FileShare.Core.Files;
using MongoDB.Bson;

namespace FileShare.AmazonS3.Files
{
    public class AmazonFileRepository : IFileRepository
    {
        static string bucket = "ht-file-share";

        public async Task<FileDto> GetFile(IMetadata metadata)
        {
            var client = new AmazonS3Client(Amazon.RegionEndpoint.USEast1);

            var request = new GetObjectRequest()
            {
                BucketName = bucket,
                Key = metadata.FileName
            };

            var response = await client.GetObjectAsync(request);
            using (Stream responseStream = response.ResponseStream)
            {
                var buffer = new byte[4096];
                using (var memoryStream = new MemoryStream())
                {
                    int read;
                    while ((read = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        await memoryStream.WriteAsync(buffer, 0, read);
                    }

                    return new FileDto(metadata, memoryStream.ToArray());
                }
            }
        }

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
                        Key = file.Metadata.Id.ToString(),
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

        public async Task DeleteFile(IMetadata metadata)
        {
            var client = new AmazonS3Client(Amazon.RegionEndpoint.USEast1);
            var request = new DeleteObjectRequest()
            {
                BucketName = bucket,
                Key = metadata.FileName,
            };

            await client.DeleteObjectAsync(request);
        }
    }
}