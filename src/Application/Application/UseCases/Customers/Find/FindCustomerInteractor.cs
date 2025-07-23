using Application.UseCases.Customers.Find.Output;
using Domain.Exceptions;
using Domain.Repositories;
using Shared.Enums;
using Shared.Utils;

namespace Application.UseCases.Customers.Find;

public class FindCustomerInteractor : IFindCustomerUseCase
{
    private readonly ICustomerRepository _customerRepository;

    public FindCustomerInteractor(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }
    
    public async Task<Result<FindCustomerOutput?>> ExecuteAsync(Guid customerId)
    {
        try
        {
            var result = await _customerRepository.FindByIdAsync(customerId);
            
            if(result.IsFailure) 
                return Result<FindCustomerOutput?>.Fail(result.ErrorMessage!, result.ErrorType ?? ErrorType.Unknown);
            
            if(result.Value == null)
                return Result<FindCustomerOutput?>.Fail(result.ErrorMessage ?? "Recurso não encontrado.", result.ErrorType ?? ErrorType.NotFound);
            
            var customerOutput = new FindCustomerOutput(result.Value!.Id, result.Value!.Name, result.Value!.Email);
            return Result<FindCustomerOutput?>.Success(customerOutput);
        }
        catch (DomainValidationException ex)
        {
            return Result<FindCustomerOutput?>.FailValidation(
                new Dictionary<string, List<string>>(ex.ValidationErrors));
        }
    }
}