using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientAPI.Domain
{
    public class ClientProfile: Entity
    {
        public int ClientProfileId { get; set; }
        public string ClientCode { get; set; }
        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Country { get; set; }
    }
}
