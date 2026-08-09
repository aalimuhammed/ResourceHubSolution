using ResourceHub.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceHub.Application.Interfaces
{
    public interface IServiceRepository
    {
        public Task<PaginatedServiceResultDto<ServiceDto>> GetPagintedServices(
            SearchFilterType  searchFilter,
            CancellationToken cancellationToken = default);
        public Task InsertNewService(
            ServiceDto serviceDto ,
            CancellationToken cancellationToken);
            
    }
}
