using Domain.Repositories;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Models;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence.DependencyInjection;

public static class PersistenceInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MssqlSettings>(configuration.GetSection("Database:MSSQL"));

        services.AddDbContext<CustomerDbContext>((sp, options) =>
        {
            var settings = sp.GetRequiredService<IOptions<MssqlSettings>>().Value;
            var connectionString = $"Server={settings.Host},{settings.Port};" +
                                   $"Database={settings.Database};" +
                                   $"User Id={settings.Username};" +
                                   $"Password={settings.Password};" +
                                   "TrustServerCertificate=true;";

            options.UseSqlServer(connectionString);
        });

        services.AddScoped<ICustomerRepository, CustomerRepository>();
        return services;
    }
}