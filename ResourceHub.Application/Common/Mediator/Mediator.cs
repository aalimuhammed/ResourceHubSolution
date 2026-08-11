namespace ResourceHub.Application.Common.Mediator
{
    public class Mediator : IMediator
    {
        private readonly IServiceProvider _serviceProvider;
        public Mediator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ;
        }
        public async Task SendCommandAsync<TRequest>(TRequest request, CancellationToken cancellationToken = default) 
            where TRequest : ICommandRequest
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var handlerType = typeof(ICommandRequestHandler<>).MakeGenericType(request.GetType());
            var handler = _serviceProvider.GetService(handlerType);
            
            if (handler == null)
                throw new InvalidOperationException($"Handler for {request.GetType().Name} not found. Please ensure the handler is registered in the service collection.");

            var method = handlerType.GetMethod("HandlerAsync");
            if (method == null)
                throw new InvalidOperationException($"Handler method not found for {request.GetType().Name}.");

            var task = (Task?)method.Invoke(handler, new object[] { request, cancellationToken });
            if (task == null)
                throw new InvalidOperationException($"Handler for {request.GetType().Name} returned null.");

            await task;
        }
        public async Task<TResponse> SendCommandAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
            where TRequest : ICommandRequest<TResponse>
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var handlerType = typeof(ICommandRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
            var handler = _serviceProvider.GetService(handlerType);
            
            if (handler == null)
                throw new InvalidOperationException($"Handler for {request.GetType().Name} not found. Please ensure the handler is registered in the service collection.");

            var method = handlerType.GetMethod("HandlerAsync");
            if (method == null)
                throw new InvalidOperationException($"Handler method not found for {request.GetType().Name}.");

            var task = (Task<TResponse>?)method.Invoke(handler, new object[] { request, cancellationToken });
            if (task == null)
                throw new InvalidOperationException($"Handler for {request.GetType().Name} returned null.");

            return await task;
        }
        public async Task<TResponse> SendQueryAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default) 
            where TRequest : IQueryRequest<TResponse>
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var handlerType = typeof(IQueryRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
            var handler = _serviceProvider.GetService(handlerType);
            
            if (handler == null)
                throw new InvalidOperationException($"Handler for {request.GetType().Name} not found. Please ensure the handler is registered in the service collection.");

            var method = handlerType.GetMethod("HandlerAsync");
            if (method == null)
                throw new InvalidOperationException($"Handler method not found for {request.GetType().Name}.");

            var task = (Task<TResponse>?)method.Invoke(handler, new object[] { request, cancellationToken });
            if (task == null)
                throw new InvalidOperationException($"Handler for {request.GetType().Name} returned null.");

            return await task;
        }
    }
}