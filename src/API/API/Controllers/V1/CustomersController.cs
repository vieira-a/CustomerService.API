using API.Controllers.Requests;
using API.Presenter.Responses;
using Application.UseCases.Customers.Create;
using Application.UseCases.Customers.Delete;
using Application.UseCases.Customers.Find;
using Application.UseCases.Customers.Update;
using Microsoft.AspNetCore.Mvc;
using Shared.Utils;

namespace API.Controllers.V1;

public sealed class CustomersController(
    ICreateCustomerUseCase createCustomerInteractor,
    IFindCustomerUseCase findCustomerInteractor,
    IUpdateCustomerUseCase updateCustomerInteractor,
    IDeleteCustomerUseCase  deleteCustomerInteractor,
    ILogger<CustomersController> logger)
    : ControlerBase(logger)
{

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateCustomerRequest request)
    {
        var input = request.ToInput();
        var result = await createCustomerInteractor.ExecuteAsync(input);

        return result.ToResponse(HttpContext);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var result = await findCustomerInteractor.ExecuteAsync(id);

        return result.ToResponse(HttpContext);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCustomerRequest request)
    {
        if (request.Name == null)
            return NoContent();

        var input = request.ToInput();
        var result = await updateCustomerInteractor.ExecuteAsync(id, input);

        return result.IsFailure
            ? Result.FromError<bool>(result).ToResponse(HttpContext)
            : result.ToResponse(HttpContext);
    }
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var result = await deleteCustomerInteractor.ExecuteAsync(id);
        
        return result.IsFailure
            ? Result.FromError<bool>(result).ToResponse(HttpContext)
            : result.ToResponse(HttpContext);
    }
}