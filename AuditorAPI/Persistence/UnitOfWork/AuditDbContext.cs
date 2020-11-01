using AuditorAPI.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace AuditorAPI.UnitOfWork
{
    public class AuditDbContext: DbContext
    {
        public AuditDbContext(DbContextOptions<AuditDbContext> options):base(options)
        {

        }
        public DbSet<AuditPortfolio> Portfolios { get; set; }
        public DbSet<AuditProfile> AuditProfiles { get; set; }
        //public DbSet<Aud>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        //{
        //    dbContextOptionsBuilder.UseSqlServer(ConfigurationManager.)
        //}
    }
}
