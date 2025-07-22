using Application.UseCases.Customers.Find.Output;

namespace Application.UseCases.Customers.Find;

public interface IFindCustomerUseCase
{
    Task<FindCustomerOutput?> ExecuteAsync(Guid customerId);
}