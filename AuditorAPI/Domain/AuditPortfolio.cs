using AuditorAPI.Domain;
using Microsoft.AspNetCore.Server.HttpSys;
using System;

namespace AuditorAPI.Domain
{
    public class AuditPortfolio :Entity, IAggregateRoot
    {
        
        public int AuditorFirmId { get; set; }
        public int AuditPortfolioId { get; set; }
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ReportReleaseDate { get; set; }

        public string DocumentName { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }


    }
}