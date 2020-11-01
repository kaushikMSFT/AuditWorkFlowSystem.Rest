using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Core
{
    public interface IProcessMessage
    {
        Task Process(AuditMessagePayload myPayload);
    }
}
