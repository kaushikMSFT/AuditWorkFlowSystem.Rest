using ClientAPI.Domain;
using ClientAPI.Persistence;
using ClientAPI.Persistence.UnitOfWork;
using ClientAPI.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ClientAPI.Services
{
    public class AuditNotiicationService
    {
        private readonly IClientUnitOfWork _unitOfWork;
        public AuditNotiicationService(IClientUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<int> PostNewAuditAsync(AuditPortfolio audit)
        {
            //var task= new Task(()=> { Thread.Sleep(1000); });
            _unitOfWork.AuditPortfolios.Add(audit);
            return await _unitOfWork.SaveAsync();

        }
    }
}
