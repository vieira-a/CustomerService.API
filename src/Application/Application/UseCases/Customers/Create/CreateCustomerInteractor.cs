using Application.Interfaces;
using Application.UseCases.Customers.Create.Input;
using Application.UseCases.Customers.Create.Output;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Shared.Enums;
using Shared.Messaging.Events;
using Shared.Utils;

namespace Application.UseCases.Customers.Create;

public sealed class CreateCustomerInteractor(
    ILogger<CreateCustomerInteractor> logger,
    ICustomerRepository repository,
    IEventPublisher eventPublisher)
    : ICreateCustomerUseCase
{
    private const string DomainExceptionMessage = "Erro de validação de domínio ao criar cliente.";
    private const string InternalExceptionMessage = "Ocorreu um erro interno. Tente novamente mais tarde.";

    public async Task<Result<CreateCustomerOutput>> ExecuteAsync(CreateCustomerInput input)
    {
        try
        {
            var customer = Customer.Create(input.Name, input.Email);

            if (input.Address != null)
            {
                var addressResult = CreateFirstAddress(input.Address);

                if (addressResult.IsFailure)
                    return Result<CreateCustomerOutput>.FailValidation(addressResult.ValidationErrors!);

                var address = addressResult.Value;
                customer.AddAddress(address!);
            }

            await repository.CreateAsync(customer);

            var customerCreatedEvent = new CustomerCreatedEvent
            {
                CustomerId = customer.Id,
                Name = customer.Name,
                Email = customer.Email
            };

            await eventPublisher.Publish(customerCreatedEvent);

            return Result<CreateCustomerOutput>.Success(
                new CreateCustomerOutput(customer.Id, customer.Name, customer.Email));
        }
        catch (DomainValidationException ex)
        {
            logger.LogError(ex, DomainExceptionMessage);
            return Result<CreateCustomerOutput>.FailValidation(
                new List<string>(ex.Errors));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, InternalExceptionMessage);
            return Result<CreateCustomerOutput>.Fail(InternalExceptionMessage, EErrorType.Internal);
        }
    }

    private Result<Address> CreateFirstAddress(AddressInput input)
    {
        try
        {
            var result = Address.Create(input.Street, input.City, input.State, input.ZipCode, input.Country,
                true);
            return Result<Address>.Success(result);
        }
        catch (DomainValidationException ex)
        {
            logger.LogError(ex, DomainExceptionMessage);
            return Result<Address>.FailValidation(
                new List<string>(ex.Errors));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, InternalExceptionMessage);
            return Result<Address>.Fail(InternalExceptionMessage, EErrorType.Internal);
        }
    }
}