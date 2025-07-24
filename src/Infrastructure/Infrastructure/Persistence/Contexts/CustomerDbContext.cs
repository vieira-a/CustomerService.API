using Infrastructure.Persistence.EntityConfigurations;
using Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Contexts;

public sealed class CustomerDbContext : DbContext
{
    public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options) {}

    public DbSet<CustomerModel> Customers => Set<CustomerModel>();
    public DbSet<AddressModel> Addresses => Set<AddressModel>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        modelBuilder.ApplyConfiguration(new AddressConfiguration());
    }

    public override int SaveChanges()
    {
        UpdateTimestamps();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateTimestamps();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateTimestamps()
    {
        var entries = ChangeTracker.Entries<BaseModel>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                if (entry.State == default)
                    entry.Entity.Created = DateTime.UtcNow;

                entry.Entity.Modified = DateTime.UtcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Property(x => x.Created).IsModified = false;
                entry.Entity.Modified = DateTime.UtcNow;
            }
        }
    }
}