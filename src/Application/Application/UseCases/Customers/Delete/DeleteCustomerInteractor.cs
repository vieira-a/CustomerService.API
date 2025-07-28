using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Shared.Enums;
using Shared.Utils;

namespace Application.UseCases.Customers.Delete;

public sealed class DeleteCustomerInteractor(
    ILogger<DeleteCustomerInteractor> logger, 
    ICustomerRepository customerRepository) : IDeleteCustomerUseCase
{
    private const string InternalExceptionMessage = "Ocorreu um erro interno. Tente novamente mais tarde.";
    
    public async Task<Result<bool>> ExecuteAsync(Guid customerId)
    {
        try
        {
            return await customerRepository.DeleteAsync(customerId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, InternalExceptionMessage);
            return Result<bool>.Fail(InternalExceptionMessage, ErrorType.Internal);
        }
        
    }
}