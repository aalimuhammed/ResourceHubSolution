using ResourceHub.Application.Common.Mediator;
using ResourceHub.Application.Dtos;

namespace ResourceHub.Application.CQRS.Query
{
    public record GetServiceWithActivityNumberQuery(string activityNumber) 
        : IQueryRequest<ServiceDto>;
}