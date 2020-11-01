using ClientAPI.Domain;
using ClientAPI.Persistence;
using ClientAPI.Persistence.Repositories;
using ClientAPI.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientAPI.UnitOfWork
{
    public class UnitOfWork:IClientUnitOfWork
    {
        private readonly ClientDbContext clientDbContext;

        public UnitOfWork(ClientDbContext dbContext)
        {
            clientDbContext = dbContext;
            if (AuditPortfolios == null)
                AuditPortfolios = new AuditPortfolioRepository(clientDbContext);
            if (ClientProfiles == null)
                ClientProfiles = new ClientProfileRepository(clientDbContext);
        }

        public IRepository<ClientProfile> ClientProfiles { get; set ; }
        public IRepository<AuditPortfolio> AuditPortfolios { get ; set ; }

        public void Save()
        {
           clientDbContext.SaveChanges();
        }

       

        public async Task<int> SaveAsync()
        {
            return await clientDbContext.SaveChangesAsync();
        }
    }


}
