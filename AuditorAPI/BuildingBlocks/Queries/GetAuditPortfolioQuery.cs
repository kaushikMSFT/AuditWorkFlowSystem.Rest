using AuditorAPI.Contracts;
using MediatR;

namespace AuditorAPI.BuildingBlocks.Queries
{
    public class GetAuditPortfolioQuery:IRequest<AuditPortfolioCreationResponse>
    {
        public int AuditId;
        public GetAuditPortfolioQuery()
        {
        }

        public GetAuditPortfolioQuery(int id)
        {
            AuditId = id;
        }
    }
}