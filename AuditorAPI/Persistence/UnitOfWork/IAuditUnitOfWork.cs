using AuditorAPI.Domain;
using AuditorAPI.Persistence;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditorAPI.UnitOfWork
{
    public interface IAuditUnitOfWork : IUnitOfWork
    {
        IRepository<AuditProfile> AuditProfiles { get; set; }
        IRepository<AuditPortfolio> AuditPortfolios { get; set; }

    }
}
