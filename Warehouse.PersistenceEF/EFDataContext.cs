using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;

namespace Warehouse.PersistenceEF
{
    public class EFDataContext : DbContext
    {
        public EFDataContext(
            string connectionString = "server=.;database=Warehouse;trusted_connection=true;") 
            : this(new DbContextOptionsBuilder<EFDataContext>()
                  .UseSqlServer(connectionString).Options)
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
