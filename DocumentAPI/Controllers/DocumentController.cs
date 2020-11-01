using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DocumentAPI.Contracts;
using DocumentAPI.Factories;
using DocumentAPI.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage.Blob;

namespace DocumentAPI.Controllers
{
    [Route("api/[controller]")]
    
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class DocumentController : ControllerBase
    {
        IDocumentUpload _uploadService;
        IStorageConnectionFactory _storageConnection;
        public DocumentController(IDocumentUpload uploadService, IStorageConnectionFactory storageConnection)
        {
            _uploadService = uploadService;
            _storageConnection = storageConnection;
        }

        // POST api/<AuditProfileController>
        /// <summary>
        /// Register a 
        /// </summary>
        /// <param name="profileRequest"></param>
        [HttpPost]

        public async Task<Boolean> PostAsync([FromForm] DocumentUploadRequest docUploadRequest)
        {
            //string todo = "createprofile";
            return await _uploadService.UploadAsync(docUploadRequest.file);
        }

        [HttpGet("{fileName}")]
        public async Task<FileStreamResult> GetAsync(string fileName)
        {
            CloudBlob blob=await _uploadService.GetAsync(fileName);
            MemoryStream ms = new MemoryStream();
            //var downloadsPathOnServer = Path.Combine(downloadPath, blob.Name);

            //using (var fileStream = File.OpenWrite(downloadsPathOnServer))
            // {
            await blob.DownloadToStreamAsync(ms);
            Stream blobStream = await blob.OpenReadAsync();
            return File(blobStream, blob.Properties.ContentType, blob.Name);
        }

    }
}
