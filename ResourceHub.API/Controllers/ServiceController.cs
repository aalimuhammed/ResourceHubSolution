
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResourceHub.Application.Common.Mediator;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceDto>>> GetServices(
            [FromQuery] SearchFilterType searchFilter ,
            CancellationToken cancellationToken = default)
        {

            var services = await _mediator.SendQueryAsync<GetServicesWithFiltersQuery, PaginatedServiceResultDto<ServiceDto>>
                (new GetServicesWithFiltersQuery(searchFilter),cancellationToken);

            if (services.ServicesDto.Any())
            {
                return Ok(services);
            }

            return NotFound("No Services found.");
        }
    }
}
