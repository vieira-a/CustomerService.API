using API.Controllers.Requests;
using API.Presenter.Responses;
using Application.UseCases.Customers.Create;
using Application.UseCases.Customers.Find;
using Application.UseCases.Customers.Update;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Shared.Utils;

namespace API.Controllers.V1;

public sealed class CustomersController : ControlerBase
{
    private readonly ICreateCustomerUseCase _createCustomerInteractor;
    private readonly IFindCustomerUseCase _findCustomerInteractor;
    private readonly IUpdateCustomerUseCase _updateCustomerInteractor;

    public CustomersController(
        ICreateCustomerUseCase createCustomerInteractor,
        IFindCustomerUseCase findCustomerInteractor,
        IUpdateCustomerUseCase updateCustomerInteractor,
        ILogger<CustomersController> logger
        ) : base(logger)
    {
        _createCustomerInteractor = createCustomerInteractor;
        _findCustomerInteractor = findCustomerInteractor;
        _updateCustomerInteractor = updateCustomerInteractor;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateCustomerRequest request)
    {
        var input = request.ToInput();
        var result = await _createCustomerInteractor.ExecuteAsync(input);

        return result.ToResponse(HttpContext);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var result = await _findCustomerInteractor.ExecuteAsync(id);

        return result.ToResponse(HttpContext);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCustomerRequest request)
    {
        if (request.Name == null)
            return NoContent();

        var input = request.ToInput();
        var result = await _updateCustomerInteractor.ExecuteAsync(id, input);

        return result.IsFailure
            ? Result.FromError<bool>(result).ToResponse(HttpContext)
            : result.ToResponse(HttpContext);
    }
}