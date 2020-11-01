using Microsoft.AspNetCore.Http;

namespace DocumentAPI.Contracts
{
    public class DocumentUploadRequest
    {
        public IFormFile file { get; set; }
    }
}