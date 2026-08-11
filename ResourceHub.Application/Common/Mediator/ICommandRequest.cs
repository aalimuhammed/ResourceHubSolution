namespace ResourceHub.Application.Common.Mediator
{
    public interface ICommandRequest { }
    public interface ICommandRequest<out TResponse> { }
}