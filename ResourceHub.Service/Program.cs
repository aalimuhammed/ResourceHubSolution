using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ResourceHub.Application.Interfaces;
using ResourceHub.Infrastructure.Contexts;
using ResourceHub.Infrastructure.Extenions;
using ResourceHub.Infrastructure.Services;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((builder, cfg) =>
    {
        cfg.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    })
    .ConfigureServices((builder, services) =>
    {
        services.AddInfarstructureService(builder.Configuration);

        services.Configure<SapSettings>(builder.Configuration.GetSection("sapsettings"));

        services.AddDbContext<ResourceHubDbContext>(opt =>
        {
            var conn = builder.Configuration.GetConnectionString("DefaultConnection");
            opt.UseMySql(conn, ServerVersion.AutoDetect(conn));
        });
    })
    .Build();

using var scope = host.Services.CreateScope();

var importer = scope.ServiceProvider
    .GetRequiredService<IResourceHubInternalService>();

await importer.ImportFromSapAsync();