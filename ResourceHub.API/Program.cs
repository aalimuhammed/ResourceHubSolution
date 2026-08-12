using ResourceHub.Application.Dtos;
using ResourceHub.Application.Extenions;
using ResourceHub.Infrastructure.Contexts;
using ResourceHub.Infrastructure.Extenions;
using ResourceHub.Infrastructure.Seed;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfarstructureService(builder.Configuration)
    .AddApplicationService();

builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("jwtsettings"));
    
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

try
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<ResourceHubDbContext>();
        await SeedData.InitializeData(context);
    }

}
catch (Exception ex)
{
    throw new Exception("An error occurred while seeding the database.", ex);
}

app.UseHttpsRedirection();

app.UseAuthentication(); 

app.UseAuthorization();

app.MapControllers();

app.Run();
