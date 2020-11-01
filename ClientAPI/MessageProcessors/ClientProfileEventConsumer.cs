
using ClientAPI.Contracts;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;


namespace ClientAPI.EventConsumers
{
    public class ClientProfileEventConsumer : IConsumer<ClientProfileCreationRequest>
    {
        public async Task Consume(ConsumeContext<ClientProfileCreationRequest> context)
        {
            ClientProfileCreationRequest profile =  context.Message as ClientProfileCreationRequest;
            
            //To further work to sync up the auditor here
        }
    }
}
