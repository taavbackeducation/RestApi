using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Warehouse.App.Models;

namespace Warehouse.Repositories.Products
{
    class ProductEntityMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(product => product.Id);

            builder.Property(_ => _.Id).IsRequired();
            builder.Property(_ => _.Title).HasMaxLength(50).IsRequired();
            builder.Property(_ => _.Price).IsRequired();
            builder.Property(_ => _.Stock).IsRequired();

            builder.HasOne(_ => _.Category)
                   .WithMany(_ => _.Products)
                   .HasForeignKey(_ => _.CategoryId)
                   .IsRequired();
        }
    }
}
