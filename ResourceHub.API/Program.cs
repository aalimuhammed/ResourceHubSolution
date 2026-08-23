using Microsoft.AspNetCore.Diagnostics;
using ResourceHub.API.Middlewares;
using ResourceHub.Application.Dtos;
using ResourceHub.Application.Extenions;
using ResourceHub.Infrastructure.Contexts;
using ResourceHub.Infrastructure.Extenions;
using ResourceHub.Infrastructure.Seed;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfarstructureService(builder.Configuration)
    .AddApplicationService();
    
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ResourceHubDbContext>();
    await SeedData.InitializeData(context);
}

app.UseMiddleware<GlobalException>();

app.UseHttpsRedirection();

app.UseAuthentication(); 

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
