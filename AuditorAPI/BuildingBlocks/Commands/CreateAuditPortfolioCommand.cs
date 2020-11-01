using AuditorAPI.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditorAPI.BuildingBlocks.Commands
{
    public class CreateAuditPortfolioCommand:IRequest<AuditPortfolioCreationResponse>
    {
        public string AuditCode { get; set; }
        public string ClientFirm { get; set; }
        public string AuditorFirm { get; set; }
        public string ReleaseDate { get; set; }

        public string Description { get; set; }

        public string Document { get; set; }

    }
}
