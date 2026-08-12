using ResourceHub.Application.Dtos;
using ResourceHub.Domain.Entities;

namespace ResourceHub.Application.Interfaces
{
    public interface IServiceRepository
    {
         Task<PaginatedServiceResultDto<ServiceDto>> GetPagintedServices(
            SearchFilterType  searchFilter,
            CancellationToken cancellationToken = default);

         Task InsertNewService(
            ServiceDto serviceDto ,
            CancellationToken cancellationToken);

         Task<bool> isActivityNoExists(string activityNumber);

         Task<Users> LoginAsync(LoginDto loginDto , CancellationToken cancellationToken);  

    }
}
