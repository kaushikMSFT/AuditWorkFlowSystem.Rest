using ClientAPI.Domain;
using ClientAPI.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientAPI.Persistence.UnitOfWork
{
    public interface IClientUnitOfWork : IUnitOfWork
    {
        IRepository<ClientProfile> ClientProfiles { get; set; }
        IRepository<AuditPortfolio> AuditPortfolios { get; set; }

    }
}
