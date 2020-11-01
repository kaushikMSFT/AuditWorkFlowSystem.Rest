using IdentityService.Contracts;
using IdentityService.Contracts.IdentityService.Contracts.V1;
using IdentityService.Contracts.v1;
using IdentityService.Providers;
using MassTransit;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class IdentityController:ControllerBase
    {
        private readonly IIdentityProvider _identityService;
        private readonly IBus _eventBus;

        public IdentityController(IIdentityProvider identityService, IBus eventBus)
        {
            _identityService = identityService;
            _eventBus = eventBus;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                });
            }


            if(request.userType.Equals("1"))
            {
                //PublishEvent(new AuditorProfileCreationRequest() { Name = request.Email });
            }
            var authResponse = await _identityService.RegisterAsync(request.Email, request.Password);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken
            });
        }

        private async void PublishEvent(AuditorProfileCreationRequest profileRequest)
        {
            //var factory = new ConnectionFactory() { HostName = "localhost" };
            //using (var connection = factory.CreateConnection())
            //using (var channel = connection.CreateModel())
            //{
            //    channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);

            //    var message = "Auditor Created";
            //    var body = Encoding.UTF8.GetBytes(message);
            //    channel.BasicPublish(exchange: "logs",
            //                         routingKey: "",
            //                         basicProperties: null,
            //                         body: body);
            //    Console.WriteLine(" [x] Sent {0}", message);
            Uri uri = new Uri("rabbitmq://localhost/Auditor-Topic");
            var endpoint = await _eventBus.GetSendEndpoint(uri);
            await endpoint.Send<AuditorProfileCreationRequest>(profileRequest);
        
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            var authResponse = await _identityService.LoginAsync(request.Email, request.Password);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken
                
            });
        }

    }

}
