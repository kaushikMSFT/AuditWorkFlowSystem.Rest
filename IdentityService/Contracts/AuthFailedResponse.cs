using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

namespace IdentityService.Controllers
{
    public class AuthFailedResponse 
    {
        public IEnumerable<string> Errors { get; set; }
    }
}