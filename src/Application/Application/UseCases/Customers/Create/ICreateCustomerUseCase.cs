namespace Application.UseCases.CreateCustomer;

public interface ICreateCustomerUseCase
{
    Task<CreateCustomerOutput> ExecuteAsync(CreateCustomerInput input);
}