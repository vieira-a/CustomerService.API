using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[ApiVersion(Version)]
[Route(Route)]
public abstract class ControlerBase : Microsoft.AspNetCore.Mvc.ControllerBase
{
    private const string Route = "api/v{version:apiVersion}/[controller]";
    private const string Version = "1";
}