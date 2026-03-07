using Scalar.AspNetCore;
using Sellify.Application.ServiceAdder;
using Sellify.Infrastructure.ServicesAdder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options=> options.SuppressModelStateInvalidFilter=true);
builder.Services.AddOpenApi();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(o=>
    {
        o.Title = "Sellify API";
        o.Theme = ScalarTheme.Moon;
        o.HideClientButton = true;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
