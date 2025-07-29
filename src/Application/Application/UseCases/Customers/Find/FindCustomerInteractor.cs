using Application.UseCases.Customers.Find.Output;
using Domain.Exceptions;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Shared.Enums;
using Shared.Utils;

namespace Application.UseCases.Customers.Find;

public sealed class FindCustomerInteractor(ILogger<FindCustomerInteractor> logger, ICustomerRepository customerRepository)
    : IFindCustomerUseCase
{
    private const string DomainExceptionMessage = "Erro de validação de domínio ao criar cliente.";
    private const string InternalExceptionMessage = "Ocorreu um erro interno. Tente novamente mais tarde.";
    private const string ResourceNotFoundExceptionMessage = "Recurso não encontrado.";

    public async Task<Result<FindCustomerOutput?>> ExecuteAsync(Guid customerId)
    {
        try
        {
            var result = await customerRepository.FindByIdAsync(customerId);
            Console.WriteLine("CUSTOMER VALUE");
            Console.WriteLine(result.Value);

            if (result.Value == null)
                return Result<FindCustomerOutput?>.Fail(ResourceNotFoundExceptionMessage, EErrorType.NotFound);

            var customerOutput = new FindCustomerOutput(result.Value!.Id, result.Value!.Name, result.Value!.Email);
            return Result<FindCustomerOutput?>.Success(customerOutput);
        }
        catch (DomainValidationException ex)
        {
            logger.LogError(ex, DomainExceptionMessage);
            return Result<FindCustomerOutput?>.FailValidation(
                new List<string>(ex.Errors));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, InternalExceptionMessage);
            return Result<FindCustomerOutput?>.Fail(InternalExceptionMessage, EErrorType.Internal);
        }
    }
}