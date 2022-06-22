using EFDemo.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFDemo.Infrastructure.Configuration;

public class StoreImageConfiguration : IEntityTypeConfiguration<StoreImage>
{
    public void Configure(EntityTypeBuilder<StoreImage> builder)
    {
        builder.ToTable(nameof(StoreImage));

        builder.HasKey(x => x.Id);
    }
}
