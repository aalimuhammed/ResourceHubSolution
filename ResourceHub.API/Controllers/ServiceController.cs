using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResourceHub.Application.Common.Mediator;
using ResourceHub.Application.CQRS.Commands;
using ResourceHub.Application.CQRS.Query;
using ResourceHub.Application.Dtos;

namespace ResourceHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ServiceController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ServiceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllServices")]
        public async Task<ActionResult<IEnumerable<ServiceResponseDto>>> GetServices(
            [FromQuery] SearchFilter searchFilter,
            CancellationToken cancellationToken = default)
        {
            var services = await _mediator.SendQueryAsync<GetServicesWithFiltersQuery,
                PaginatedResultDto<ServiceResponseDto>>
                (new GetServicesWithFiltersQuery(searchFilter), cancellationToken);

            return Ok(services);
        }

        [HttpGet("GetByActivityNumber")]
        public async Task<ActionResult<ServiceDto>> GetServiceByActivityNumber(
             [FromQuery] string activityNumber,
             CancellationToken cancellationToken = default)
        {

                var service = await _mediator.SendQueryAsync<GetServiceWithActivityNumberQuery, ServiceDto>
                (new GetServiceWithActivityNumberQuery(activityNumber), cancellationToken);

                return Ok(service);

        }

        [HttpPost("CreateNewService")]
        public async Task<ActionResult> InsertNewServiceController(
            [FromBody] ServiceDto serviceDto,
            CancellationToken cancellationToken = default)
        {

                await _mediator.SendCommandAsync(new InsertNewServiceCommand(serviceDto, cancellationToken));
                return Ok("The Service is Inserted Successfully");
        }
    }
}