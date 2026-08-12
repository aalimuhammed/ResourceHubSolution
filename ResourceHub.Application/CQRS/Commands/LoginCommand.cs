using ResourceHub.Application.Common.Mediator;
using ResourceHub.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceHub.Application.CQRS.Commands
{
    public record LoginCommand(LoginDto LoginDto) : ICommandRequest<string>;
}
