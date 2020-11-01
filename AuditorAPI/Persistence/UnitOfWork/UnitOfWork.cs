using AuditorAPI.Domain;
using AuditorAPI.Persistence;
using AuditorAPI.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditorAPI.UnitOfWork
{
    public class UnitOfWork:IAuditUnitOfWork
    {
        private readonly AuditDbContext auditDbContext;

        public IRepository<AuditPortfolio> AuditPortfolios { get;  set; }
        public IRepository<AuditProfile> AuditProfiles { get;  set; }


        public UnitOfWork(AuditDbContext dbContext)
        {
            auditDbContext = dbContext;
           
            if(AuditPortfolios==null)
                AuditPortfolios = new AuditPortfolioRepository(auditDbContext);
            if(AuditProfiles == null)
                AuditProfiles = new AuditorProfileRepository( auditDbContext);
        }

        public void Save()
        {
            auditDbContext.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await auditDbContext.SaveChangesAsync();
        }
    }


}
