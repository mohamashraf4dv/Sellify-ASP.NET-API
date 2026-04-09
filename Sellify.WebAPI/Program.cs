using FluentValidation;
using Scalar.AspNetCore;
using Sellify.Application.Features.Authentication.Commands.InternalUserLogin;
using Sellify.Application.ServiceAdder;
using Sellify.Infrastructure.AppSettingOptions;
using Sellify.Infrastructure.ServicesAdder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options=> options.SuppressModelStateInvalidFilter=true);
builder.Services.AddOpenApi();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(builder.Configuration.GetSection("URLS").Get<URLS>().ClientDomain)
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(o=>
    {
        o.Title = "Sellify API";
        o.Theme = ScalarTheme.Saturn;
        o.HideClientButton = true;
    });
}
app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();

app.Run();
