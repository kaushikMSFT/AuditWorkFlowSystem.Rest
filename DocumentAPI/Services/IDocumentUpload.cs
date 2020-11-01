using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentAPI.Services
{
    public interface IDocumentUpload
    {
        Task<Boolean> UploadAsync(IFormFile file);

        Task<CloudBlob> GetAsync(string blobName);

        Task DeleteImage(string name);


       
    }
}
