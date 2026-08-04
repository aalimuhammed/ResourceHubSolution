
using ResourceHub.Application.CQRS.Query;
using ResourceHub.Application.CQRS.Query.Handlers;
using ResourceHub.Application.Extenions;
using ResourceHub.Infrastructure.Extenions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddInfarstructureService(builder.Configuration);

builder.Services.AddApplicationService();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddMediatR(typeof(GetServiceWithFiltersHandler));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
