﻿using ClientAPI.UnitOfWork;
using ClientAPI.Domain;
using ClientAPI.Persistence;
using ClientAPI.Persistence.UnitOfWork;
using ClientAPI.Services;
using ClientAPI.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Services
{
    public class AuditPortfolioService : IAuditPortfolioService
    {
        private readonly IClientUnitOfWork _unitOfWork;
        private readonly IRepository<AuditPortfolio> _portfoliorepository;
        public AuditPortfolioService(IClientUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //_portfoliorepository = auditPortfolioRepository;
        }

        public void Create(AuditPortfolio portfolio)
        {
            _unitOfWork.AuditPortfolios.Add(portfolio);
            _unitOfWork.Save();
        }

        public async Task<int> CreateAsync(AuditPortfolio portfolio)
        {
            _unitOfWork.AuditPortfolios.Add(portfolio);
            return await _unitOfWork.SaveAsync();
        }

        public  IEnumerable<AuditPortfolio> GetAll()
        {
           return  _unitOfWork.AuditPortfolios.GetAll();
        }

        public async Task<IEnumerable<AuditPortfolio>> GetAllAsync()
        {
            return await _unitOfWork.AuditPortfolios.GetAllAsync();
        }

        public AuditPortfolio GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<AuditPortfolio> GetByIdAsync(int id)
        {
            return await _unitOfWork.AuditPortfolios.GetAsync(id);
        }
    }
}
