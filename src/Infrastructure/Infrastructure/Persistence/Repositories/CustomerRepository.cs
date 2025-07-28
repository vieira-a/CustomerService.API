using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared.Enums;
using Shared.Utils;

namespace Infrastructure.Persistence.Repositories;

public sealed class CustomerRepository(ILogger<CustomerRepository> logger, CustomerDbContext context)
    : ICustomerRepository
{
    private const string DatabaseExceptionMessage = "Erro de banco de dados.";
    private const string InternalExceptionMessage = "Ocorreu um erro interno. Tente novamente mais tarde.";
    private const string ResourceNotFoundExceptionMessage = "Recurso não encontrado.";

    public async Task<Result> CreateAsync(Customer customer)
    {
        try
        {
            var customerModel = CustomerMapper.MapFromDomain(customer);

            context.Customers.Add(customerModel);
            await context.SaveChangesAsync();
            return Result.Success();
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, DatabaseExceptionMessage);
            return Result.Fail(DatabaseExceptionMessage, ErrorType.Database);
        }

        catch (Exception ex)
        {
            logger.LogError(ex, InternalExceptionMessage);
            return Result.Fail(InternalExceptionMessage, ErrorType.Internal);
        }
    }

    public async Task<Result<Customer?>> FindByIdAsync(Guid customerId)
    {
        try
        {
            var customer = await context.Customers.FindAsync(customerId);
            var mappedCustomer = customer == null ? null : CustomerMapper.MapFromEntity(customer);

            return Result<Customer?>.Success(mappedCustomer);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, DatabaseExceptionMessage);
            return Result<Customer?>.Fail(DatabaseExceptionMessage, ErrorType.Database);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, InternalExceptionMessage);
            return Result<Customer?>.Fail(InternalExceptionMessage, ErrorType.Internal);
        }
    }

    public async Task<Result<bool>> UpdateAsync(Customer customer)
    {
        try
        {
            var customerModel = await context.Customers.FindAsync(customer.Id);
            if (customerModel == null)
                return Result<bool>.Fail(ResourceNotFoundExceptionMessage, ErrorType.NotFound);

            context.Entry(customerModel).CurrentValues.SetValues(customer);
            await context.SaveChangesAsync();

            return Result<bool>.Success(true);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, DatabaseExceptionMessage);
            return Result<bool>.Fail(DatabaseExceptionMessage, ErrorType.Database);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, InternalExceptionMessage);
            return Result<bool>.Fail(InternalExceptionMessage, ErrorType.Internal);
        }
    }

    public async Task<Result<bool>> DeleteAsync(Guid customerId)
    {
        try
        {
            var entity = await context.Customers.FindAsync(customerId);
            if (entity == null)
                return Result<bool>.Fail(ResourceNotFoundExceptionMessage, ErrorType.NotFound);

            context.Customers.Remove(entity);
            await context.SaveChangesAsync();

            return Result<bool>.Success(true);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, DatabaseExceptionMessage);
            return Result<bool>.Fail(DatabaseExceptionMessage, ErrorType.Database);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, InternalExceptionMessage);
            return Result<bool>.Fail(InternalExceptionMessage, ErrorType.Internal);
        }
    }
}