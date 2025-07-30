using Application.UseCases.Customers.Create.Input;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Shared.Enums;
using Shared.Utils;

namespace Application.UseCases.Customers.CreateAddress;

public class CreateAddressInteractor(
    ILogger<CreateAddressInteractor> logger,
    ICustomerRepository customerRepository) : ICreateAddressUseCase
{
    private const string DomainExceptionMessage = "Erro de validação de domínio ao criar cliente.";
    private const string InfrastructureExceptionMessage = "Erro interno inesperado ao criar cliente.";
    private const string InternalExceptionMessage = "Ocorreu um erro interno. Tente novamente mais tarde.";
    
    public async Task<Result<bool>> ExecuteAsync(Guid customerId, AddressInput input)
    {
        try
        {
            var customer = await customerRepository.FindWithAddressAsync(customerId);
            var newAddress = Address.Create(input.Street, input.City, input.State, input.ZipCode, input.Country, input.IsMain);
            
            customer?.AddAddress(newAddress);
            
            var result = await customerRepository.CreateAddressAsync(customerId, newAddress); ;
            
            return result.IsSuccess ? Result<bool>.Success(true) : Result<bool>.Fail(InternalExceptionMessage, EErrorType.Database);
        }
        catch (DomainValidationException ex)
        {
            Console.WriteLine($"EX.ERRORS: {ex.Errors[0]}");
            logger.LogError(ex, DomainExceptionMessage);
            return Result<bool>.FailValidation(new List<string>(ex.Errors));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, InfrastructureExceptionMessage);
            return Result<bool>.Fail(InternalExceptionMessage, EErrorType.Internal);
        }
    }
}