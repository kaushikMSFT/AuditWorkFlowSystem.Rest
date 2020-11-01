using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestRabbitMQMassTransit_Send.Contracts;

namespace TestRabbitMQMassTransit_Send.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishEventController : ControllerBase
    {
        public IBusControl _bus;

        public PublishEventController(IBusControl bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuditor()
        {
            Uri uri = new Uri("rabbitmq://localhost/Auditor-Topic");
            var endpoint = await _bus.GetSendEndpoint(uri);
            await endpoint.Send<AuditorProfile>(new  { Name = "Kaushik B" });
            return Ok("Auditor Created");

        }
    }
}
