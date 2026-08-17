using ResourceHub.Application.Dtos;
using ResourceHub.Domain.Entities;

namespace ResourceHub.Application.Interfaces
{
    public interface IServiceRepository
    {
         Task<PaginatedServiceResultDto<ServiceResponseDto>> GetPagintedServices(
            SearchFilter searchFilter,
            CancellationToken cancellationToken = default);

         Task InsertNewService(
            ServiceDto serviceDto ,
            CancellationToken cancellationToken);

         Task<bool> IsActivityNoExists(string activityNumber , CancellationToken cancellationToken =default);

    }
}
