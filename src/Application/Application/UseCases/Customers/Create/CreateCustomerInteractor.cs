using Application.Exceptions;
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

public class CreateCustomerInteractor(
    ILogger<CreateCustomerInteractor> logger,
    ICustomerRepository repository,
    IEventPublisher eventPublisher)
    : ICreateCustomerUseCase
{
    private const string DomainExceptionMessage = "Erro de validação de domínio ao criar cliente.";
    private const string InfrastructureExceptionMessage = "Erro interno inesperado ao criar cliente.";
    private const string InternalExceptionMessage = "Ocorreu um erro interno. Tente novamente mais tarde.";
    private const string DatabaseExceptionMessage = "Ocorreu um erro no banco de dados.";

    public async Task<Result<CreateCustomerOutput>> ExecuteAsync(CreateCustomerInput input)
    {
        try
        {
            var customer = Customer.Create(input.Name, input.Email);

            if (input.Address != null)
            {
                var addressResult = CreateAddress(input.Address);

                if (addressResult.IsFailure)
                    return Result<CreateCustomerOutput>.FailValidation(addressResult.ValidationErrors!);

                var address = addressResult.Value;
                customer.AddAddress(address!);
            }

            var result = await repository.CreateAsync(customer);

            if (result.IsFailure)
                return Result<CreateCustomerOutput>.Fail(result.ErrorMessage!, result.ErrorType ?? ErrorType.Unknown);

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
                new Dictionary<string, List<string>>(ex.ValidationErrors));
        }
        catch (DatabaseException ex)
        {
            logger.LogError(ex, DatabaseExceptionMessage);
            return Result<CreateCustomerOutput>.Fail(DatabaseExceptionMessage, ErrorType.Database);
        }
        catch (InternalServerException ex)
        {
            logger.LogError(ex, InfrastructureExceptionMessage);
            return Result<CreateCustomerOutput>.Fail(InfrastructureExceptionMessage, ErrorType.Infrastructure);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, InternalExceptionMessage);
            return Result<CreateCustomerOutput>.Fail(InternalExceptionMessage, ErrorType.Internal);
        }
    }

    private Result<Address> CreateAddress(AddressInput input)
    {
        try
        {
            var result = Address.Create(input.Street, input.City, input.State, input.ZipCode, input.Country);
            return Result<Address>.Success(result);
        }
        catch (DomainValidationException ex)
        {
            logger.LogError(DomainExceptionMessage);
            return Result<Address>.FailValidation(
                new Dictionary<string, List<string>>(ex.ValidationErrors));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, InfrastructureExceptionMessage);
            return Result<Address>.Fail(InternalExceptionMessage, ErrorType.Internal);
        }
    }
}