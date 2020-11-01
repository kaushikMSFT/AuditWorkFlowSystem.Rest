using AuditorAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AuditorAPI.Persistence.Repositories
{
    public class MockAuditProfileRepository : IRepository<AuditProfile>
    {
        public void Add(AuditProfile entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<AuditProfile> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AuditProfile> Find(Expression<Func<AuditProfile, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public AuditProfile Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AuditProfile> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AuditProfile>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AuditProfile> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(AuditProfile entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<AuditProfile> entities)
        {
            throw new NotImplementedException();
        }

        public AuditProfile SingleOrDefault(Expression<Func<AuditProfile, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
