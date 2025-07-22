using Application.UseCases.Customers.Create;
using Application.UseCases.Customers.Find;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection;

public static class ApplicationInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICreateCustomerUseCase, CreateCustomerInteractor>();    
        services.AddScoped<IFindCustomerUseCase, FindCustomerInteractor>(); 
        return services;
    }
    
}