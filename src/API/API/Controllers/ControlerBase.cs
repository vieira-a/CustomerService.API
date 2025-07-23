using Microsoft.AspNetCore.Mvc;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace API.Controllers
{
    [ApiController]
    [ApiVersion(Version)]
    [Route(Route)]
    public abstract class ControlerBase : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        protected readonly ILogger Logger;

        private const string Route = "api/v{version:apiVersion}/[controller]";
        private const string Version = "1";

        protected ControlerBase(ILogger logger)
        {
            Logger = logger;
        }
    }
}