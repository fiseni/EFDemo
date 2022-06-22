using EFDemo.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFDemo.Infrastructure.Configuration;

public class StoreConfiguration : IEntityTypeConfiguration<Store>
{
    public void Configure(EntityTypeBuilder<Store> builder)
    {
        builder.ToTable(nameof(Store));

        builder.OwnsOne(x => x.Address, ab =>
        {
            ab.Property(x => x.Street).HasColumnName(nameof(Address.Street)).HasMaxLength(255);
            ab.Property(x => x.City).HasColumnName(nameof(Address.City)).HasMaxLength(255);
        });

        builder.HasMany(x => x.Products).WithOne(x => x.Store).HasForeignKey(x => x.StoreId);

        builder.HasOne(x => x.Image).WithOne(x => x.Store).HasForeignKey<StoreImage>(x => x.StoreId);

        builder.HasKey(x => x.Id);
    }
}
