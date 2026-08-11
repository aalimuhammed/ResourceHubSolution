using ResourceHub.Application.Common.Mediator;
using ResourceHub.Application.Dtos;
using ResourceHub.Application.Interfaces;

namespace ResourceHub.Application.CQRS.Query.Handlers
{
    internal class GetServiceWithFiltersHandler:IQueryRequestHandler
        <GetServicesWithFiltersQuery , 
        PaginatedServiceResultDto<ServiceDto>>
    {
        private readonly IServiceRepository _serviceRepository;
        public GetServiceWithFiltersHandler(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }
        public async Task<PaginatedServiceResultDto<ServiceDto>> HandlerAsync(
            GetServicesWithFiltersQuery request, 
            CancellationToken cancellationToken)
        {
            var services = await _serviceRepository.GetPagintedServices(
                request.SearchFilterType, 
                cancellationToken);

            return services;
        }
    }
}