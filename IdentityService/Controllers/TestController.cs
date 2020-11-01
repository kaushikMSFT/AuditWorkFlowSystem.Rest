using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.Controllers
{
    public class TestController: Controller
    {
        [HttpGet("api/test")]
        public IActionResult GetResult()
        {
            return Ok(new { name = "Kaushik" });
        }
    }
}
