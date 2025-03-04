
using SubmissionService.API;
using NLog.Web;
using Microsoft.IdentityModel.Logging;
using SubmissionService.Infrastructure;
using SubmissionService.Application;
using System.Reflection;
using MediatR;
using SubmissionService.Application.Features.Cache.Query;



var builder = WebApplication.CreateBuilder(args);
// Use NLog for logging
builder.Logging.ClearProviders();
builder.Host.UseNLog();

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);
//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
IdentityModelEventSource.ShowPII = true; // Enable detailed error messages
// Use the Startup class
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    
    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
    await mediator.Send(new CacheWarmUpQuery());

}
startup.Configure(app);

app.Run();

