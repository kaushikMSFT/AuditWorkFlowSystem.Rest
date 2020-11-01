using AuditorAPI.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuditorAPI.Services
{
    public interface IAuditPortfolioService
    {
        void Create(AuditPortfolio portfolio);

        Task<int> CreateAsync(AuditPortfolio portfolio);
        IEnumerable<AuditPortfolio> GetAll();

        Task<IEnumerable<AuditPortfolio>> GetAllAsync();

        AuditPortfolio GetById(int id);

        Task<AuditPortfolio> GetByIdAsync(int id);
    }
}