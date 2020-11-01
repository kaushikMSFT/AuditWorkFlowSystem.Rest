using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientAPI.Domain
{
    public class AuditPortfolio : Entity, IAggregateRoot
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
