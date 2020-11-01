
using AuditorAPI.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditorAPI.BuildingBlocks.Queries
{
    public class GetAllAuditPortfoliosQuery:IRequest<List<AuditPortfolioCreationResponse>>
    {
        
    }
}
