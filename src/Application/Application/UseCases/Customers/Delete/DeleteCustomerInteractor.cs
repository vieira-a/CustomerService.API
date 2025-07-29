using Application.Interfaces;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Shared.Enums;
using Shared.Messaging.Events;
using Shared.Utils;

namespace Application.UseCases.Customers.Delete;

public sealed class DeleteCustomerInteractor(
    ILogger<DeleteCustomerInteractor> logger, 
    ICustomerRepository customerRepository, IEventPublisher eventPublisher) : IDeleteCustomerUseCase
{
    private const string InternalExceptionMessage = "Ocorreu um erro interno. Tente novamente mais tarde.";
    
    public async Task<Result<bool>> ExecuteAsync(Guid customerId)
    {
        try
        {
            var result = await customerRepository.DeleteAsync(customerId);

            var customerDeletedEvent = new CustomerDeletedEvent
            {
                CustomerId = customerId,
            };
            
            await eventPublisher.Publish(customerDeletedEvent);

            return result;

        }
        catch (Exception ex)
        {
            logger.LogError(ex, InternalExceptionMessage);
            return Result<bool>.Fail(InternalExceptionMessage, EErrorType.Internal);
        }
        
    }
}