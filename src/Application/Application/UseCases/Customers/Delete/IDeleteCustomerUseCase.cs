using Shared.Utils;
namespace Application.UseCases.Customers.Delete;

public interface IDeleteCustomerUseCase
{
    Task<Result<bool>> ExecuteAsync (Guid customerId);
}