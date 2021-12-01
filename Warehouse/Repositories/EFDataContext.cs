using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using Warehouse.App.Models;
using Warehouse.Repositories.Products;

namespace Warehouse.Repositories
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


        public override ChangeTracker ChangeTracker
        {
            get
            {
                var tracker = base.ChangeTracker;
                tracker.LazyLoadingEnabled = false;
                tracker.AutoDetectChangesEnabled = true;
                tracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
                return tracker;
            }
        }
    }
}
