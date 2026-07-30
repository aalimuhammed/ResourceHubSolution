using ResourceHub.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceHub.Application.Interfaces
{
    public interface IResourceHubExternalService
    {
        public Task<ServicePageResult> GetServicePageAsync( int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}
