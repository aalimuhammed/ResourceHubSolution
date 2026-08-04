using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ResourceHub.Application.Interfaces;
using ResourceHub.Infrastructure.Contexts;
using ResourceHub.Infrastructure.Repositories;
using ResourceHub.Infrastructure.Services;
using ResourceHub.Infrastructure.UOW;

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
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
               .UseSnakeCaseNamingConvention());


            services.AddHttpClient<IResourceHubExternalService, ResourceHubExternalService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IServiceRepository,ServiceRepository>();

            services.AddScoped<IResourceHubInternalService, ResourceHubInternalService>();

            return services;
        }
    }
}
