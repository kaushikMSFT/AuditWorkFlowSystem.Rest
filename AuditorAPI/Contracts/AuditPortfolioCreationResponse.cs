using System;

namespace AuditorAPI.Contracts
{
    public class AuditPortfolioCreationResponse
    {
        public int AuditPortfolioId { get; set; }
        public int ClientId { get; set; }

        public int AuditorFirmId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ReportReleaseDate { get; set; }

        public string Document { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
    }
}