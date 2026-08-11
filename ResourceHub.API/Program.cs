
using ResourceHub.Application.Extenions;
using ResourceHub.Infrastructure.Extenions;

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
