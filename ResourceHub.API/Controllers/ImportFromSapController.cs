using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResourceHub.Application.Interfaces;

namespace ResourceHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportFromSapController : ControllerBase
    {
        private readonly IResourceHubInternalService _resourceHubInternalService;

        public ImportFromSapController(IResourceHubInternalService resourceHubInternalService)
        {
            _resourceHubInternalService = resourceHubInternalService;
        }

        [HttpPost("import")]
        public async Task<IActionResult> ImportFromSap(CancellationToken cancellationToken)
        {
            await _resourceHubInternalService.ImportFromSapAsync();

            return Ok("Services imported successfully");
        }
    }
}
