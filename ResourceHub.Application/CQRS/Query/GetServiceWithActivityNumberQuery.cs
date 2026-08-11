using ResourceHub.Application.Common.Mediator;
using ResourceHub.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceHub.Application.CQRS.Query
{
    public record GetServiceWithActivityNumberQuery(string activityNumber) 
        : IQueryRequest<ServiceDto>;
    
}
