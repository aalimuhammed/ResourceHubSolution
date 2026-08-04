namespace ResourceHub.Application.Common.Mediator
{
    public interface ICommandRequestHandler<in TRequest>
        where TRequest : ICommandRequest
    {
        Task HandlerAsync(TRequest request, CancellationToken cancellationToken = default);
    }
    public interface ICommandRequestHandler<in TRequest, TResponse>
        where TRequest : ICommandRequest<TResponse>
    {
        Task<TResponse> HandlerAsync(TRequest request, CancellationToken cancellationToken = default);
    }
}