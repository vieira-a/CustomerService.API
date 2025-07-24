using Application.Interfaces;
using Application.UseCases.Customers.Update.Input;
using Domain.Exceptions;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Shared.Enums;
using Shared.Messaging.Events;
using Shared.Utils;

namespace Application.UseCases.Customers.Update;

public sealed class UpdateCustomerInteractor(
    ILogger<UpdateCustomerInteractor> logger,
    ICustomerRepository customerRepository,
    IEventPublisher eventPublisher) : IUpdateCustomerUseCase
{
    private const string DomainExceptionMessage = "Erro de validação de domínio ao criar cliente.";
    private const string InfrastructureExceptionMessage = "Erro interno inesperado ao criar cliente.";
    private const string InternalExceptionMessage = "Ocorreu um erro interno. Tente novamente mais tarde.";
    private const string ResourceNotFoundExceptionMessage = "Recurso não encontrado.";

    public async Task<Result<bool>> ExecuteAsync(Guid customerId, UpdateCustomerInput? input)
    {
        try
        {
            var result = await customerRepository.FindByIdAsync(customerId);

            if (result.Value == null)
                return Result<bool>.Fail(ResourceNotFoundExceptionMessage, ErrorType.NotFound);

            var customer = result.Value;

            if (input != null) customer.UpdateName(input.Name);

            await customerRepository.UpdateAsync(customer);

            var customerUpdatedEvent = new CustomerUpdatedEvent
            {
                CustomerId = customer.Id,
                Name = customer.Name
            };

            await eventPublisher.Publish(customerUpdatedEvent);
            return Result<bool>.Success(true);
        }
        catch (DomainValidationException ex)
        {
            logger.LogError(ex, DomainExceptionMessage);
            return Result<bool>.FailValidation(new List<string>(ex.Errors));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, InfrastructureExceptionMessage);
            return Result<bool>.Fail(InternalExceptionMessage, ErrorType.Internal);
        }
    }
}