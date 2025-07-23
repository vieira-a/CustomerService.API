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

    public async Task<Result<Customer?>> FindByIdAsync(Guid customerId)
    {
        try
        {
            var customer = await _context.Customers.FindAsync(customerId);
            var mappedCustomer = customer == null ? null : CustomerMapper.MapFromEntity(customer);
            
            return Result<Customer?>.Success(mappedCustomer);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Erro de banco de dados.");
            return Result<Customer?>.Fail("Erro de banco de dados.", ErrorType.Database);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro inesperado.");
            return Result<Customer?>.Fail("Erro inesperado.", ErrorType.Unknown);
        }
    }

    public async Task<Result<bool>> UpdateAsync(Customer customer)
    {
        try
        {
            var customerModel = await _context.Customers.FindAsync(customer.Id);
            if (customerModel == null) 
                return Result<bool>.Fail("Cliente não encontrado.", ErrorType.NotFound);
        
            _context.Entry(customerModel).CurrentValues.SetValues(customer);
            await _context.SaveChangesAsync();
            
            return Result<bool>.Success(true);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Erro de banco de dados.");
            return Result<bool>.Fail("Erro de banco de dados.", ErrorType.Database);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro inesperado.");
            return Result<bool>.Fail("Erro inesperado.", ErrorType.Unknown);
        }
        
    }
}