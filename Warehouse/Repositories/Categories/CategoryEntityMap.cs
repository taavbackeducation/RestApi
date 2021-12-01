using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Warehouse.App.Models;

namespace Warehouse.Repositories.Categories
{
    class CategoryEntityMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(_ => _.Title).IsRequired().HasMaxLength(50);

            builder.HasMany(_ => _.Products)
                   .WithOne(_ => _.Category)
                   .HasForeignKey(_ => _.CategoryId)
                   .IsRequired();
        }
    }
}
