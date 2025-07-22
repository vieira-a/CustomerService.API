using API.Controllers.Requests;
using Application.UseCases.Customers.Create;
using Application.UseCases.Customers.Find;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1;

public sealed class CustomersController : ControlerBase
{
    private readonly ICreateCustomerUseCase  _createCustomerInteractor;
    private readonly IFindCustomerUseCase _findCustomerInteractor;

    public CustomersController(ICreateCustomerUseCase createCustomerInteractor, IFindCustomerUseCase findCustomerInteractor)
    {
        _createCustomerInteractor = createCustomerInteractor;
        _findCustomerInteractor = findCustomerInteractor;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsyc([FromBody] CreateCustomerRequest request)
    {
        var input = request.ToInput();
        var result = await _createCustomerInteractor.ExecuteAsync(input);

        return CreatedAtAction(nameof(CreateAsyc), new { id = result.CustomerId }, result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var result = await _findCustomerInteractor.ExecuteAsync(id);
        return result == null ? NotFound() : Ok(result);
    }
}