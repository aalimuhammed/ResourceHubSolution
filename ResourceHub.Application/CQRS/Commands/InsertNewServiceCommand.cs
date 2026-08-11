using ResourceHub.Application.Common.Mediator;
using ResourceHub.Application.CQRS.Commands.Handlers;
using ResourceHub.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceHub.Application.CQRS.Commands
{
    public record InsertNewServiceCommand(
        ServiceDto ServiceDto ,
        CancellationToken CancellationToken):ICommandRequest; 
}
