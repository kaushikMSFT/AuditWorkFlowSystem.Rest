using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditorAPI.UnitOfWork
{
    public interface IUnitOfWork
    {
        public void Save();
        public Task<int> SaveAsync();
    }
}
