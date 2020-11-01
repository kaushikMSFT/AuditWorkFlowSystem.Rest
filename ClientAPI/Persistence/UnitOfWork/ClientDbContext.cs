using ClientAPI.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace ClientAPI.UnitOfWork
{
    public class ClientDbContext: DbContext
    {
        public ClientDbContext(DbContextOptions<ClientDbContext> options):base(options)
        {

        }
        
        public DbSet<ClientProfile> ClientProfiles { get; set; }
        public DbSet<AuditPortfolio>AuditPortfolios { get; set; }
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
