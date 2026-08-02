using ResourceHub.Application.Dtos;

namespace ResourceHub.Application.Interfaces
{
    public interface IResourceHubExternalService
    {
         Task<ServicePageResult> GetServicePageAsync(
             int pageNumber,
             int pageSize, 
             CancellationToken cancellationToken = default);
    }
}
