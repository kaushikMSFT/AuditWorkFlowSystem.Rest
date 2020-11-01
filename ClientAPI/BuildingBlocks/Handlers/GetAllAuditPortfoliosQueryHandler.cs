using ClientAPI.BuildingBlocks.Queries;
using ClientAPI.Contracts;
using ClientAPI.Domain;
using ClientAPI.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ClientAPI.BuildingBlocks.Handlers
{
    public class GetAllAuditPortfoliosQueryHandler : IRequestHandler<GetAllAuditPortfoliosQuery, List<AuditPortfolioCreationResponse>>
    {
        IAuditPortfolioService _auditPortfolioService = null;

        public GetAllAuditPortfoliosQueryHandler(IAuditPortfolioService auditPortfolioService)
        {
            this._auditPortfolioService = auditPortfolioService;
        }

        public async Task<List<AuditPortfolioCreationResponse>> Handle(GetAllAuditPortfoliosQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<AuditPortfolio> audits= await _auditPortfolioService.GetAllAsync();
            List<AuditPortfolioCreationResponse> auditPortfolioCreationResponses = new List<AuditPortfolioCreationResponse>();
            audits.ToList().ForEach(x => { auditPortfolioCreationResponses.Add(new AuditPortfolioCreationResponse() {  ClientId = x.ClientId, AuditorFirmId = x.AuditorFirmId, AuditPortfolioId = x.AuditPortfolioId, CreatedBy = x.CreatedBy, Name = x.Name, Description = x.Description, ReportReleaseDate = x.ReportReleaseDate, Document=x.DocumentName }); });
            return auditPortfolioCreationResponses;
        }
    }
}
