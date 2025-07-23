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
    private readonly ICreateCustomerUseCase  _createCustomerInteractor;
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
        Logger.LogInformation("Criando novo cliente");
        
        var input = request.ToInput();
        var result = await _createCustomerInteractor.ExecuteAsync(input);

        Logger.LogInformation("Novo cliente com nome {RequestName} e e-mail {RequestEmail} criado com Id {CustomerId}", request.Name, request.Email, result.Value!.CustomerId);
        
        return result.ToResponse(HttpContext);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        Logger.LogInformation("Buscando cliente pelo {ID}", id);
        
        var result = await _findCustomerInteractor.ExecuteAsync(id);
        
        Logger.LogInformation("Finalizada busca do cliente pelo {ID}", id);
        
        return result.ToResponse(HttpContext);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCustomerRequest request)
    {
        Logger.LogInformation("Atualizando dados do cliente com {ID}", id);
        
        if (request.Name == null)
            return NoContent();
        
        var input = request.ToInput();
        var result = await _updateCustomerInteractor.ExecuteAsync(id, input);

        Logger.LogInformation("Finalizando atualização de dados do cliente com {ID}", id);

        return result.IsFailure ? Result.FromError<bool>(result).ToResponse(HttpContext) : result.ToResponse(HttpContext);
    }
}