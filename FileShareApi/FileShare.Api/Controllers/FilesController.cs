using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using FileUpload.Core.Files;
using FileUpload.Directory;

namespace FileUploadApi.Controllers
{
    public class FilesController : ApiController
    {
        private readonly IFileService fileService;

        public FilesController()
        {
            this.fileService = new DirectoryFileService();
        }

        // GET api/files
        /// <summary>
        /// Get All
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<FileDto>> Get()
        {
            return await fileService.GetAvailableFileNames();
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
            var localFilePath = await fileService.GetFile(key);
            var fileName = Path.GetFileName(localFilePath);

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(new FileStream(localFilePath, FileMode.Open, FileAccess.Read));
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") {FileName = fileName};
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(MimeMapping.GetMimeMapping(fileName));
            
            return response;
        }

        public async Task<IHttpActionResult> Post()
        {
            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);
            foreach (var content in provider.Contents)
            {
                
                var fileName = content.Headers.ContentDisposition.FileName.Trim('\"');
                var fileData = await content.ReadAsByteArrayAsync();

                var file = new FileDto(fileName, fileData);

                await fileService.SaveFile(file);
            }

            return Ok();
        }


        // DELETE api/files/5
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="key"></param>
        [Route("files/{key}")]
        public void Delete(string key)
        {
            fileService.DeleteFile(key);
        }
    }

    // We implement MultipartFormDataStreamProvider to override the filename of File which
    // will be stored on server, or else the default name will be of the format like Body-
    // Part_{GUID}. In the following implementation we simply get the FileName from 
    // ContentDisposition Header of the Request Body.
    public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        public CustomMultipartFormDataStreamProvider(string path) : base(path) { }

        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            return headers.ContentDisposition.FileName.Replace("\"", string.Empty);
        }
    }
}
