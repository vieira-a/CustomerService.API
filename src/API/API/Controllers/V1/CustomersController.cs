using API.Controllers.Requests;
using Application.UseCases.Customers.Create;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1;

public sealed class CustomersController : ControlerBase
{
    private readonly ICreateCustomerUseCase  _createCustomerInteractor;

    public CustomersController(ICreateCustomerUseCase createCustomerInteractor)
    {
        _createCustomerInteractor = createCustomerInteractor;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsyc([FromBody] CreateCustomerRequest request)
    {
        var input = request.ToInput();
        var result = await _createCustomerInteractor.ExecuteAsync(input);
        Console.WriteLine($"Street in Input: {input.Address?.Street}");

        return CreatedAtAction(nameof(CreateAsyc), new { id = result.CustomerId }, result);
    }
}