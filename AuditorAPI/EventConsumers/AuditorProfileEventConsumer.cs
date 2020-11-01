using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using AuditorAPI.Contracts;

namespace AuditorAPI.EventConsumers
{
    public class AuditorProfileEventConsumer : IConsumer<AuditorProfileCreationRequest>
    {
        public async Task Consume(ConsumeContext<AuditorProfileCreationRequest> context)
        {
            AuditorProfileCreationRequest profile =  context.Message as AuditorProfileCreationRequest;
            
            //To further work to sync up the auditor here
        }
    }
}
