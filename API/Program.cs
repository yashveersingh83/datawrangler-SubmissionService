
using SubmissionService.API;
using NLog.Web;
using Microsoft.IdentityModel.Logging;



var builder = WebApplication.CreateBuilder(args);
// Use NLog for logging
builder.Logging.ClearProviders();
builder.Host.UseNLog();

IdentityModelEventSource.ShowPII = true; // Enable detailed error messages
// Use the Startup class
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);
var app = builder.Build();
startup.Configure(app);

app.Run();

