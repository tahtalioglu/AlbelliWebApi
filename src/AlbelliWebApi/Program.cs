using FluentValidation.AspNetCore;
using AlbelliWebApi.Infrastructure.Extensions;
using AlbelliWebApi.Infrastructure.Persistance.EFCore.Repository;
using AlbelliWebApi.Infrastructure.Validators;
using Microsoft.AspNetCore.Mvc;
using AlbelliWebApi.Filters;

var builder = WebApplication.CreateBuilder(args);
var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .AddJsonFile($"appsettings.{environmentName}.json")
                            .AddEnvironmentVariables()
                            .Build();
// Add services to the container.

builder.Services.AddHealthChecks();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.UseEFContext(configuration);
builder.Services.AddScoped<AlbelliDataContext>();

builder.Services.AddApplicationRepositories();
builder.Services.AddApplicationServices();
builder.Services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<OrderRequestValidator>());
builder.Services.AddMvc().AddMvcOptions(o =>
{
    o.Filters.Add(typeof(GlobalExceptionFilter));
    o.Filters.Add(typeof(ModelStateFilter));
}).AddMvcOptions(option => option.EnableEndpointRouting = false)
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<OrderRequestValidator>());

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks("/healthcheck");
    endpoints.MapControllers();
});
app.UseHttpsRedirection();
app.UseCors(options => options.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader());
app.UseSwagger();
app.UseSwaggerUI();



app.Run();