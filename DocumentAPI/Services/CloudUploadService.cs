using DocumentAPI.Factories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
using File = System.IO.File;
using Microsoft.Extensions.FileProviders;

namespace DocumentAPI.Services
{
    public class CloudUploadService : IDocumentUpload
    {
        private readonly IStorageConnectionFactory _storageConnectionFactory;

        public CloudUploadService(IStorageConnectionFactory storageConnectionFactory)
        {
            _storageConnectionFactory = storageConnectionFactory;
        }
        public Task DeleteImage(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<CloudBlob> GetAsync(string blobName)
        {
            
            var blobContainer = await _storageConnectionFactory.GetContainer();
            CloudBlob blob= blobContainer.GetBlockBlobReference(blobName);
            
            //Stream blobStream = await blob.OpenReadAsync();

            return blob;

            
           // }
        }

       

        public async Task<Boolean> UploadAsync(IFormFile file)
        {
            var stream = file.OpenReadStream();
            var blobContainer = await _storageConnectionFactory.GetContainer();
            var thumbnailWidth = 100;
            var extension = Path.GetExtension(file.FileName);
            //var encoder = GetEncoder(extension);
            CloudBlockBlob blob = blobContainer.GetBlockBlobReference(GetRandomBlobName(file.FileName));
            try
            {
                //var stream = file.OpenReadStream();
               // {
                    //using (var output = new MemoryStream())
                    //using (Image image = Image.Load(stream))
                    //{
                    //    var divisor = image.Width / thumbnailWidth;
                    //    var height = Convert.ToInt32(Math.Round((decimal)(image.Height / divisor)));

                    //    image.Mutate(x => x.Resize(thumbnailWidth, height));
                    //    image.Save(output, encoder);
                    //    output.Position = 0;
                    await blob.UploadFromStreamAsync(stream);
                    //}
               // }
            }
            catch(Exception e)
            {
                string s = e.Message;
            }
            return true;
        }

        /// <summary> 
        /// string GetRandomBlobName(string filename): Generates a unique random file name to be uploaded  
        /// </summary> 
        private string GetRandomBlobName(string filename)
        {
            string ext = Path.GetExtension(filename);
            return string.Format("{0}", filename);
           // return string.Format("{0:10}_{1}{2}", DateTime.Now.Ticks, Guid.NewGuid(), ext);
        }


    }
}
