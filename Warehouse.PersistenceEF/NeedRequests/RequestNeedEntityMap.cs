using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Warehouse.Entities;

namespace Warehouse.PersistenceEF.NeedRequests
{
    class RequestNeedEntityMap : IEntityTypeConfiguration<RequestNeed>
    {
        public void Configure(EntityTypeBuilder<RequestNeed> builder)
        {
            builder.ToTable("RequestNeeds");

            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(_ => _.Section).IsRequired();
            builder.Property(_ => _.Count).IsRequired();

            builder.HasOne(_ => _.Product)
                   .WithMany(_ => _.RequestNeeds)
                   .HasForeignKey(_ => _.ProductId)
                   .IsRequired();
        }
    }
}
