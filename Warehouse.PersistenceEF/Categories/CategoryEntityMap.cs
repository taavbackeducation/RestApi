using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.DataEncryption;
using Microsoft.EntityFrameworkCore;
using Warehouse.Entities;

namespace Warehouse.PersistenceEF.Categories
{
    class CategoryEntityMap : SecureEntityMap, IEntityTypeConfiguration<Category>
    {
        public CategoryEntityMap(IEncryptionProvider provider) : base(provider) { }

        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(_ => _.Title)
                   .IsRequired()
                   .HasMaxLength(50)
                   .IsEncrypted(_provider);

            builder.HasMany(_ => _.Products)
                   .WithOne(_ => _.Category)
                   .HasForeignKey(_ => _.CategoryId)
                   .IsRequired();
        }
    }
}
