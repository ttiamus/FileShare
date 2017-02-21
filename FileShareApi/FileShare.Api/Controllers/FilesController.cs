using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using FileShare.AmazonS3.Files;
using FileShare.Core.Files;
using FileShare.Directory;
using Microsoft.Ajax.Utilities;
using MongoDB.Bson;

namespace FileShare.Api.Controllers
{
    public class FilesController : ApiController
    {
        private readonly IFileService fileService;

        public FilesController()
        {
            this.fileService = new AmazonFileService();
        }

        // GET api/files
        /// <summary>
        /// Get All
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<IMetadata>> Get()
        {
            return await fileService.GetFileNames();
        }

        // GET api/files/5
        /// <summary>
        /// Get by Id
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [Route("files/{key}")]
        public async Task<HttpResponseMessage> Get(string key)
        {
            var fileTask = fileService.GetFile(ObjectId.Parse(key));
            //var fileName = Path.GetFileName(localFilePath);

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            
            var file = await fileTask;
            response.Content = new StreamContent(new MemoryStream(file.FileBytes));
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") {FileName = file.Metadata.FileName};
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(file.Metadata.MimeType);
            
            return response;
        }


        /* This was inteded to take multiple files at once, but that's a problem with updates.
        public async Task Post()
        {
            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);

            var fileList = new List<FileDto>();
            foreach (var content in provider.Contents)
            {
                var fileName = content.Headers.ContentDisposition.FileName.Trim('\"');
                var fileData = await content.ReadAsByteArrayAsync();

                fileList.Add(new FileDto(fileName, fileData, MimeMapping.GetMimeMapping(fileName)));
            }
            await fileService.SaveFiles(fileList);
        }*/

        //currently only supports uploading 1 file at a time
        public async Task Post()
        {
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var provider = await Request.Content.ReadAsMultipartAsync(new InMemoryMultipartFormDataStreamProvider());

            var formData = provider.FormData;

            var file = provider.Files.First();
            var fileName = file.Headers.ContentDisposition.FileName.Trim('\"');
            var fileData = await file.ReadAsByteArrayAsync();

            if(formData["id"].IsNullOrWhiteSpace())
                await fileService.SaveFile(new FileDto(fileName, fileData, MimeMapping.GetMimeMapping(fileName)));
            else
                await fileService.SaveFile(new FileDto(formData["id"], fileName, fileData, MimeMapping.GetMimeMapping(fileName)));
        }

        // DELETE api/files/5
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="key"></param>
        [Route("files/{key}")]
        public async Task Delete(string key)
        {
            await fileService.DeleteFile(ObjectId.Parse(key));
        }
    }

}
