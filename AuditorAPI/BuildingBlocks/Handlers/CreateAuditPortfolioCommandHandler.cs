using AuditorAPI.BuildingBlocks.Commands;
using AuditorAPI.Contracts;
using AuditorAPI.Domain;
using AuditorAPI.Services;
using MediatR;
using Messaging.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AuditorAPI.BuildingBlocks.Handlers
{
    public class CreateAuditPortfolioCommandHandler : IRequestHandler<CreateAuditPortfolioCommand, AuditPortfolioCreationResponse>
    {
        IAuditPortfolioService _auditPortfolioService;
        private readonly MessageProducer _serviceBusTopicSender;
        public CreateAuditPortfolioCommandHandler(IAuditPortfolioService auditPortfolioService, MessageProducer messageProducer)
        {
            _auditPortfolioService = auditPortfolioService;
            _serviceBusTopicSender = messageProducer;
        }

        public async Task<AuditPortfolioCreationResponse> Handle(CreateAuditPortfolioCommand request, CancellationToken cancellationToken)
        {
            await _auditPortfolioService.CreateAsync(new AuditPortfolio()
            {
                AuditorFirmId = Int32.Parse(request.AuditorFirm),
                ClientId = Int32.Parse(request.ClientFirm),
                DocumentName = request.Document,
                Description = request.Description,
                //Name=request.n
                ReportReleaseDate = DateTime.Parse(request.ReleaseDate),
                //Name = request.Name
            });

            // Send this to the bus for the other services
            await _serviceBusTopicSender.SendMessage(new AuditMessagePayload
            {
                AuditorId = request.AuditorFirm,
                AuditName = request.Description,
                ClientId = request.ClientFirm,
                DocumentName = request.Document
                
            });

            
            return new AuditPortfolioCreationResponse();
        }
    }
}
