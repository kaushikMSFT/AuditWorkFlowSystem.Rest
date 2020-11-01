using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using TestRabbitMQMassTransit_Send.Contracts;

namespace TestRabbitMQMassTransit_Receive.Consumers
{
    public class AuditorProfileEventConsumer : IConsumer<AuditorProfile>
    {
        public async Task Consume(ConsumeContext<AuditorProfile> context)
        {
            var profile =  context.Message;
            
            //return profile;
        }
    }
}
