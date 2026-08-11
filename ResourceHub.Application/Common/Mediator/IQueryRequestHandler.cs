namespace ResourceHub.Application.Common.Mediator
{
    public interface IQueryRequestHandler<in TRequest, TResponse>
        where TRequest : IQueryRequest<TResponse>
    {
        Task<TResponse> HandlerAsync(TRequest request, CancellationToken cancellationToken);
    }
}