
using SubmissionService.API;
using NLog.Web;
using Microsoft.IdentityModel.Logging;
using SharedKernel;
using System.Reflection;
using SubmissionService.Infrastructure;
using SubmissionService.Application;



var builder = WebApplication.CreateBuilder(args);
// Use NLog for logging
builder.Logging.ClearProviders();
builder.Host.UseNLog();

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);
IdentityModelEventSource.ShowPII = true; // Enable detailed error messages
// Use the Startup class
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);
var app = builder.Build();
startup.Configure(app);

app.Run();

