using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared.Enums;
using Shared.Utils;

namespace Infrastructure.Persistence.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly ILogger <CustomerRepository> _logger;
    private readonly CustomerDbContext  _context;

    public CustomerRepository(ILogger <CustomerRepository> logger, CustomerDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<Result> CreateAsync(Customer customer)
    {
        try
        {
            var customerModel = CustomerMapper.MapFromDomain(customer);
            
            _context.Customers.Add(customerModel);
            await _context.SaveChangesAsync();
            return Result.Success();
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Erro de banco de dados.");
            return Result.Fail("Erro de banco de dados.", ErrorType.Database);
        }
        
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro inesperado.");
            return Result.Fail("Erro inesperado.", ErrorType.Unknown);
        }
    }

    public async Task<Customer?> FindByIdAsync(Guid customerId)
    {
        var customer = await _context.Customers.FindAsync(customerId);
        return customer == null ? null : CustomerMapper.MapFromEntity(customer);
    }

    public async Task UpdateAsync(Customer customer)
    {
        var customerModel = await _context.Customers.FindAsync(customer.Id);
        if (customerModel == null) return;
        
        _context.Entry(customerModel).CurrentValues.SetValues(customer);
        await _context.SaveChangesAsync();
    }
}