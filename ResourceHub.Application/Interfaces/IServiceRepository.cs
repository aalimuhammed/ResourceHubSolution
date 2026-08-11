using ResourceHub.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
