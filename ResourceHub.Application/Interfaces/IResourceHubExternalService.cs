using ResourceHub.Application.Dtos;

namespace ResourceHub.Application.Interfaces
{
    public interface IResourceHubExternalService
    {
         Task<PaginatedResultDto<ServiceDto>> GetServicePageAsync(
             int pageNumber,
             int pageSize, 
             CancellationToken cancellationToken = default);
    }
}
