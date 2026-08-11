
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResourceHub.Application.Common.Mediator;
using ResourceHub.Application.CQRS.Commands;
using ResourceHub.Application.CQRS.Query;
using ResourceHub.Application.CQRS.Query.Handlers;
using ResourceHub.Application.Dtos;
using ResourceHub.Application.Interfaces;
using ResourceHub.Infrastructure.Repositories;

namespace ResourceHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ServiceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("getallservices")]
        public async Task<ActionResult<IEnumerable<ServiceDto>>> GetServices(
            [FromQuery] SearchFilterType searchFilter,
            CancellationToken cancellationToken = default)
        {

            var services = await _mediator.SendQueryAsync<GetServicesWithFiltersQuery,
                PaginatedServiceResultDto<ServiceDto>>
                (new GetServicesWithFiltersQuery(searchFilter), cancellationToken);

            if (services.ServicesDto.Any())
            {
                return Ok(services);
            }

            return NotFound("No Services found.");
        }

        [HttpGet("getbyactivitynumber")]
        public async Task<ActionResult<ServiceDto>> GetServiceByActivityNumber(
            [FromQuery] string activityNumber,
             CancellationToken cancellationToken
            )
        {
            try
            {
                var service = await _mediator.SendQueryAsync<GetServiceWithActivityNumberQuery, ServiceDto>
                (new GetServiceWithActivityNumberQuery(activityNumber), cancellationToken);
                if (service != null)
                {
                    return Ok(service);
                }
                return NotFound("No Service found.");
            }
            catch (Exception ex){
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("createnewservice")]
        public async Task<ActionResult> InsertNewServiceController(
            [FromBody] ServiceDto serviceDto,
            CancellationToken cancellationToken = default)
        {
            try
            {
                await _mediator.SendCommandAsync(new InsertNewServiceCommand(serviceDto, cancellationToken));
                return Ok("The Service is Inserted Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"There is a problem while adding the service {ex.Message}");
            }
        }
    }
}
