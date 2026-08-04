namespace ResourceHub.Application.Common.Mediator
{
    public interface IMediator
    {
        Task SendCommandAsync<TRequest>(TRequest request, CancellationToken cancellationToken = default)
            where TRequest : ICommandRequest;
        Task<TResponse> SendCommandAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
            where TRequest : ICommandRequest<TResponse>;
        Task<TResponse> SendQueryAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
            where TRequest : IQueryRequest<TResponse>;
    }
}