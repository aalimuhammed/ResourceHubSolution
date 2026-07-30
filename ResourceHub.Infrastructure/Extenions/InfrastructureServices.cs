using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ResourceHub.Application.Interfaces;
using ResourceHub.Infrastructure.Contexts;
using ResourceHub.Infrastructure.Services;
using ResourceHub.Infrastructure.UOK;

namespace ResourceHub.Infrastructure.Extenions
{
    public static class InfrastructureServices
    {
        public static IServiceCollection AddInfarstructureService(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ResourceHubDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            services.AddHttpClient<IResourceHubExternalService, ResourceHubExternalService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IResourceHubInternalService, ResourceHubInternalService>();

            return services;
        }
    }
}
