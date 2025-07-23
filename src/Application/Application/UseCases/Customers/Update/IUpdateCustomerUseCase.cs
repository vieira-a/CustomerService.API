using Application.UseCases.Customers.Update.Input;
using Shared.Utils;

namespace Application.UseCases.Customers.Update;

public interface IUpdateCustomerUseCase
{
    Task<Result<bool>> ExecuteAsync(Guid customerId, UpdateCustomerInput? input);
}