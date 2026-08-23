using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ResourceHub.Application.Dtos;
using ResourceHub.Application.Interfaces;
using ResourceHub.Infrastructure.Contexts;
using ResourceHub.Infrastructure.Repositories;
using ResourceHub.Infrastructure.Services;
using ResourceHub.Infrastructure.UOW;
using System.Text;

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

            services.Configure<JwtSettings>(
            configuration.GetSection("jwtsettings"));

            services.AddHttpClient<IResourceHubExternalService, ResourceHubExternalService>();

            services.AddSingleton<IJwtTokenGenerator, TokenService>();

            services.AddSingleton<IPasswordService, PasswordService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IServiceRepository,ServiceRepository>();

            services.AddScoped<IResourceHubInternalService, ResourceHubInternalService>();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"])),
                   ValidateIssuer = true,
                   ValidIssuer = configuration["JwtSettings:issuer"],
                   ValidateAudience = true,
                   ValidAudience = configuration["JwtSettings:audience"],
                   ValidateLifetime = true,
               });
            services.AddAuthorization();

            return services;
        }
    }
}