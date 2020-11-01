﻿using ClientAPI.Domain;
using ClientAPI.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientAPI.Persistence.Repositories
{
    public class AuditPortfolioRepository : Repository<AuditPortfolio>
    {
        public AuditPortfolioRepository(ClientDbContext context)
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

        public ClientDbContext AuditContext
        {
            get { return Context as ClientDbContext; }
        }
    }
}


