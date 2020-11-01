using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestRabbitMQMassTransit_Send.Contracts
{
    public interface AuditorProfile
    {
        public string Name { get; set; }
    }
}
