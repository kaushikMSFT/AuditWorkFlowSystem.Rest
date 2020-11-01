using AuditorAPI.BuildingBlocks.Queries;
using AuditorAPI.Contracts;
using AuditorAPI.Domain;
using AuditorAPI.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AuditorAPI.BuildingBlocks.Handlers
{
    public class GetAuditPortfolioQueryHandler : IRequestHandler<GetAuditPortfolioQuery, AuditPortfolioCreationResponse>
    {
        IAuditPortfolioService _auditPortfolioService = null;

        public GetAuditPortfolioQueryHandler(IAuditPortfolioService auditPortfolioService)
        {
            this._auditPortfolioService = auditPortfolioService;
        }

        public async Task<AuditPortfolioCreationResponse> Handle(GetAuditPortfolioQuery request, CancellationToken cancellationToken)
        {
            AuditPortfolio audit = await _auditPortfolioService.GetByIdAsync(request.AuditId);
            AuditPortfolioCreationResponse auditPortfolioCreationResponse = new AuditPortfolioCreationResponse()
            { ClientId = audit.ClientId, AuditorFirmId=audit.AuditorFirmId, AuditPortfolioId = audit.AuditPortfolioId, CreatedBy = audit.CreatedBy, Name = audit.Name, Description = audit.Description, Document=audit.DocumentName, ReportReleaseDate = audit.ReportReleaseDate };
            return auditPortfolioCreationResponse;
        }
    }
}
