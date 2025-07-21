using Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations;

public class AddressConfiguration : IEntityTypeConfiguration<AddressModel>
{
    public void Configure(EntityTypeBuilder<AddressModel> builder)
    {
        builder.ToTable("ADDRESSES");
        
        builder.HasKey(a => a.AddressId);

        builder.Property(a => a.Street).HasMaxLength(200);
        builder.Property(a => a.City).HasMaxLength(100);
        builder.Property(a => a.State).HasMaxLength(100);
        builder.Property(a => a.ZipCode).HasMaxLength(20);
        builder.Property(a => a.Country).HasMaxLength(100);
    }
}