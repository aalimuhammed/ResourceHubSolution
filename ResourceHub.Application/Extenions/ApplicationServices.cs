using Microsoft.Extensions.DependencyInjection;
using ResourceHub.Application.Common.Mediator;
using ResourceHub.Application.Validators;
using System.Reflection;
using FluentValidation;
namespace ResourceHub.Application.Extenions
{
    public static class ApplicationServices
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddMediator(Assembly.GetExecutingAssembly());

            services.AddValidatorsFromAssemblyContaining<CreateServiceDtoValidator>();

            return services;
        }
        
        private static IServiceCollection AddMediator(this IServiceCollection services, Assembly assembly)
        {
            services.AddScoped<IMediator, Mediator>();

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
