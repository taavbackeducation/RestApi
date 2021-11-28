using Microsoft.EntityFrameworkCore;
using System;
using webAPI3.App.Models;

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


        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            EntityMapOnProductModel(modelBuilder);
        }

        private static void EntityMapOnProductModel(ModelBuilder modelBuilder)
        {
            var product = modelBuilder.Entity<Product>();

            product.HasKey(product => product.Id);
            product.Property(_ => _.Id).IsRequired();
            product.Property(_ => _.Title).HasMaxLength(50).IsRequired();
            product.Property(_ => _.Price).IsRequired();
            product.Property(_ => _.Stock).IsRequired();
        }
    }
}
