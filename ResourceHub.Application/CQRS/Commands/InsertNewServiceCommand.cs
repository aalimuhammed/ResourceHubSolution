using ResourceHub.Application.Common.Mediator;
using ResourceHub.Application.Dtos;

namespace ResourceHub.Application.CQRS.Commands
{
    public record InsertNewServiceCommand(
        ServiceDto ServiceDto ,
        CancellationToken CancellationToken) : ICommandRequest; 
}