using Microsoft.Extensions.DependencyInjection;
using ResourceHub.Application.Common.Mediator;
using System.Reflection;

namespace ResourceHub.Application.Extenions
{
    public static class ApplicationServices
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddMediator(Assembly.GetExecutingAssembly());

            return services;
        }
        private static IServiceCollection AddMediator(this IServiceCollection services, Assembly assembly)
        {
            // Register the mediator implementation
            services.AddScoped<IMediator, Mediator>();

            // Register all command handlers (void return)
            var commandHandlerTypes = assembly.GetTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType &&
                    i.GetGenericTypeDefinition() == typeof(ICommandRequestHandler<>)))
                .ToList();

            foreach (var handlerType in commandHandlerTypes)
            {
                var interfaceType = handlerType.GetInterfaces()
                    .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandRequestHandler<>));
                services.AddScoped(interfaceType, handlerType);
            }

            // Register all command handlers (with return value)
            var commandHandlerWithResponseTypes = assembly.GetTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType &&
                    i.GetGenericTypeDefinition() == typeof(ICommandRequestHandler<,>)))
                .ToList();

            foreach (var handlerType in commandHandlerWithResponseTypes)
            {
                var interfaceType = handlerType.GetInterfaces()
                    .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandRequestHandler<,>));
                services.AddScoped(interfaceType, handlerType);
            }

            // Register all query handlers
            var queryHandlerTypes = assembly.GetTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType &&
                    i.GetGenericTypeDefinition() == typeof(IQueryRequestHandler<,>)))
                .ToList();

            foreach (var handlerType in queryHandlerTypes)
            {
                var interfaceType = handlerType.GetInterfaces()
                    .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IQueryRequestHandler<,>));
                services.AddScoped(interfaceType, handlerType);
            }

            return services;
        }
    }
}
