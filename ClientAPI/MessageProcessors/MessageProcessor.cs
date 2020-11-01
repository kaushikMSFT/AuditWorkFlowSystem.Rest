using ClientAPI.Domain;
using ClientAPI.Persistence.UnitOfWork;
using ClientAPI.Services;
using ClientAPI.UnitOfWork;
using Messaging.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientAPI.MessageProcessors
{
    public class MessageProcessor : IProcessMessage
    {
        IClientUnitOfWork clientUnitOfWork = null;
        public MessageProcessor(IClientUnitOfWork unitOfWork )
        {
            clientUnitOfWork = unitOfWork;
        }
        public async Task Process(AuditMessagePayload messagePayload)
        {
            AuditNotiicationService service = new AuditNotiicationService(clientUnitOfWork);
            await service.PostNewAuditAsync(new AuditPortfolio() { AuditorFirmId = Int32.Parse(messagePayload.AuditorId), ClientId = Int32.Parse(messagePayload.ClientId) , Description=messagePayload.AuditName, DocumentName=messagePayload.DocumentName});
        }
    }
}
