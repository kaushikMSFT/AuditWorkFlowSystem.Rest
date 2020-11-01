using System;
using System.Collections.Generic;
using System.Text;

namespace Messaging.Core
{
    public class AuditMessagePayload
    {
        public string AuditId { get; set; }
        public string AuditName { get; set; }
        public string AuditorId { get; set; }
        public string ClientId { get; set; }

        public string DocumentName { get; set; }
    }
}
