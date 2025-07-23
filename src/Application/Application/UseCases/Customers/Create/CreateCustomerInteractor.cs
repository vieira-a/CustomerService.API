using Microsoft.Extensions.Logging;

using Application.Interfaces;
using Application.UseCases.Customers.Create.Input;
using Application.UseCases.Customers.Create.Output;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Shared.Enums;
using Shared.Messaging.Events;
using Shared.Utils;

namespace Application.UseCases.Customers.Create;

public class CreateCustomerInteractor : ICreateCustomerUseCase 
{
    private readonly ILogger <CreateCustomerInteractor> _logger;
    private readonly ICustomerRepository _repository;
    private readonly IEventPublisher  _eventPublisher;

    public CreateCustomerInteractor(ILogger <CreateCustomerInteractor> logger, ICustomerRepository repository, IEventPublisher  eventPublisher)
    {
        _logger = logger;
        _repository = repository;
        _eventPublisher = eventPublisher;
    }
    
    public async Task<Result<CreateCustomerOutput>> ExecuteAsync(CreateCustomerInput input)
    {
        try
        {
            var customer = Customer.Create(input.Name, input.Email);

            if (input.Address != null)
            {
                var address = CreateAddress(input.Address);
                customer.AddAddress(address);
            }

            await _repository.CreateAsync(customer);

            var customerCreatedEvent = new CustomerCreatedEvent
            {
                CustomerId = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
            };

            await _eventPublisher.Publish(customerCreatedEvent);

            return Result<CreateCustomerOutput>.Success(
                new CreateCustomerOutput(customer.Id, customer.Name, customer.Email));
        }
        catch (DomainValidationException ex)
        {
            return Result<CreateCustomerOutput>.FailValidation(new Dictionary<string, List<string>>(ex.ValidationErrors));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar novo cliente.");
            return Result<CreateCustomerOutput>.Fail("Erro ao criar novo cliente.", ErrorType.Unknown);
        }
    }

    private static Address CreateAddress(AddressInput input)
    {
        return Address.Create(input.Street, input.City, input.State, input.ZipCode, input.Country);
    }
}