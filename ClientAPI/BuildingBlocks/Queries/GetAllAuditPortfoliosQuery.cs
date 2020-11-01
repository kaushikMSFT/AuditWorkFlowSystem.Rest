
using ClientAPI.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientAPI.BuildingBlocks.Queries
{
    public class GetAllAuditPortfoliosQuery:IRequest<List<AuditPortfolioCreationResponse>>
    {
        
    }
}
