using Aranda.Productos.Api.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Aranda.Productos.Api.Controllers
{
    /// <summary>
    /// Abstract class BaseApiController
    /// </summary>

    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ProducesErrorResponseType(typeof(ErrorResponse))]
    public abstract class BaseApiController : ControllerBase
    {
        /// <summary>
        /// logger
        /// </summary>
        protected readonly ILogger<BaseApiController> _logger;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="logger">The logger</param>
        public BaseApiController(ILogger<BaseApiController> logger)
        {
            _logger = logger;
        }
    }
}
