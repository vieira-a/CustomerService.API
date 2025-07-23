using Application.UseCases.Customers.Find.Output;
using Shared.Utils;

namespace Application.UseCases.Customers.Find;

public interface IFindCustomerUseCase
{
    Task<Result<FindCustomerOutput?>> ExecuteAsync(Guid customerId);
}