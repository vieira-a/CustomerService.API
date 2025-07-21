using Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations;

public class CustomerConfiguration : IEntityTypeConfiguration<CustomerModel>
{
    public void Configure(EntityTypeBuilder<CustomerModel> builder)
    {
        builder.ToTable("CUSTOMERS");
        builder.HasKey(c => c.CustomerId);

        builder.Property(c => c.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.Email)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasMany(c => c.Addresses)
            .WithOne(a => a.Customer)
            .HasForeignKey(a => a.CustomerId);
    }
}