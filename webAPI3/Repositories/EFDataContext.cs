using Microsoft.EntityFrameworkCore;
using System;
using webAPI3.App.Models;
using webAPI3.Repositories.Products;

namespace webAPI3.Repositories
{
    public class EFDataContext : DbContext
    {
        public EFDataContext() 
            : this(new DbContextOptionsBuilder<EFDataContext>()
                  .UseSqlServer("server=.;database=Warehouse;trusted_connection=true;").Options)
        {
        }

        private EFDataContext(DbContextOptions<EFDataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EFDataContext).Assembly);
        }
    }
}
