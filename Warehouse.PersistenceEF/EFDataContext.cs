using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DataEncryption;
using Microsoft.EntityFrameworkCore.DataEncryption.Providers;
using Warehouse.PersistenceEF.Categories;
using System;

namespace Warehouse.PersistenceEF
{
    public class EFDataContext : DbContext
    {
        private readonly byte[] _encryptionKey;
        private readonly byte[] _encryptionIV;
        private readonly IEncryptionProvider _provider;

        public EFDataContext(
            string connectionString = "server=.;database=Warehouse;trusted_connection=true;") 
            : this(new DbContextOptionsBuilder<EFDataContext>()
                  .UseSqlServer(connectionString).Options)
        {
            _encryptionKey = Convert.FromBase64String("UfQ07dY+cMa8/bCrL1kp0ADdRt6CLLFnh7u0eZdBj3w=");
            _encryptionIV = Convert.FromBase64String("wvCQ/yem5Jf2Y87Lmv56dA==");
            _provider = new AesProvider(_encryptionKey, _encryptionIV);
        }

        private EFDataContext(DbContextOptions<EFDataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseEncryption(_provider);
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CategoryEntityMap(_provider));
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EFDataContext).Assembly,
                _ => _.IsAssignableFrom(typeof(SecureEntityMap)));
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
