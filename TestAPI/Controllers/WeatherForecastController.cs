using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("AllowOrigin")]
    public class WeatherForecastController : ControllerBase
    {
        public static List<AuditPortfolio> audits = new List<AuditPortfolio>() {
            new AuditPortfolio { AuditCode="1", Description = "Audit1", AuditorFirm="E&Y" , ClientFirm="Cognizant", ReleaseDate="2020,11,1"},
            new AuditPortfolio { AuditCode="2", Description = "Audit2", AuditorFirm="E&Y" , ClientFirm="Cognizant", ReleaseDate="2020,12,1"}
            };

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<AuditPortfolio> Get()
        {
           //ar rng = new Random();
            return audits
            .ToArray();
        }

        [HttpPost]
        public IEnumerable<AuditPortfolio> Create(AuditPortfolio audit)
        {
            audits.Add(audit);
            return audits;
        }

        public class AuditPortfolio
        {
            public string AuditCode { get; set; }
            public string AuditorFirm { get; set; }
            public string ClientFirm { get; set; }
            public string ReleaseDate { get; set; }
            public string Description { get; set; }
        }
    }
}
