using Application.UseCases.Customers.Create.Input;
using Application.UseCases.Customers.Create.Output;
using Shared.Utils;

namespace Application.UseCases.Customers.Create;

public interface ICreateCustomerUseCase
{
    Task<Result<CreateCustomerOutput>> ExecuteAsync(CreateCustomerInput input);
}
