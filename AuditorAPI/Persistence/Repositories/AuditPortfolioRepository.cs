using AuditorAPI.Domain;

using AuditorAPI.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditorAPI.Persistence
{
    public class AuditPortfolioRepository : Repository<AuditPortfolio>
    {
        public AuditPortfolioRepository(AuditDbContext context)
            : base(context)
        {
        }

        //public IEnumerable<AuditProfile> GetTopSellingCourses(int count)
        //{
        //    return Context.AuditProfiles.OrderByDescending(c => c.FullPrice).Take(count).ToList();
        //}

        //public IEnumerable<AuditProfile> GetCoursesWithAuthors(int pageIndex, int pageSize = 10)
        //{
        //    return PlutoContext.Courses
        //        .Include(c => c.Author)
        //        .OrderBy(c => c.Name)
        //        .Skip((pageIndex - 1) * pageSize)
        //        .Take(pageSize)
        //        .ToList();
        //}

        public AuditDbContext AuditContext
        {
            get { return Context as AuditDbContext; }
        }
    }
}
