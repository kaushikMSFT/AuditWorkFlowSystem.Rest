using AuditorAPI.Domain;
using AuditorAPI.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

namespace AuditorAPI.Persistence.UnitOfWork
{
    public class MockUnitOfWork : IUnitOfWork
    {
        public IRepository<AuditPortfolio> AuditPortfolios { get; set; }
        public IRepository<AuditProfile> AuditProfiles { get; set; }
        public void Save()
        {
            return;
        }

        public Task<int> SaveAsync()
        {
            throw new NotImplementedException();
        }
    }
}
