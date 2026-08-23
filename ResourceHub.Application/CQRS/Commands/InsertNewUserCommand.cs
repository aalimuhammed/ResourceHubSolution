using ResourceHub.Application.Common.Mediator;
using ResourceHub.Application.Dtos;

namespace ResourceHub.Application.CQRS.Commands
{
    public record InsertNewUserCommand(CreateUserDto UserDto) : ICommandRequest;
}