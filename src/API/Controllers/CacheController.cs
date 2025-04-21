using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SubmissionService.Application.Features.Cache.Query;
using System.Threading.Tasks;

namespace SubmissionService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CacheController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CacheController> _logger;


        public CacheController(IMediator mediator, ILogger<CacheController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("load")]
        public async Task<IActionResult> LoadCache()
        {
            _logger.LogInformation("Cache load initiated.");
            var result = await _mediator.Send(new CacheWarmUpQuery());
            if (result)
            {
                _logger.LogInformation("Cache loaded successfully.");
                return Ok("Cache loaded successfully.");
            }

            _logger.LogError("Cache load failed.");
            return StatusCode(500, "Cache load failed.");
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteAllCache()
        {
            _logger.LogInformation("Cache delete initiated.");
            var result = await _mediator.Send(new DeleteAllCacheQuery());
            if (result)
            {
                _logger.LogInformation("Cache deleted successfully.");
                return Ok("Cache deleted successfully.");
            }

            _logger.LogError("Cache delete failed.");
            return StatusCode(500, "Cache delete failed.");
        }
    }
}
