using ResourceHub.Application.Dtos;

namespace ResourceHub.Application.Interfaces
{
    public interface IServiceRepository
    {
         Task<PaginatedResultDto<ServiceResponseDto>> GetPagintedServices(
            SearchFilter searchFilter,
            CancellationToken cancellationToken = default);
         Task InsertNewService(
            ServiceDto serviceDto ,
            CancellationToken cancellationToken = default);
         Task<bool> IsActivityNoExists(
             string activityNumber , 
             CancellationToken cancellationToken =default);
    }
}