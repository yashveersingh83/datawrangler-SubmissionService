
using SubmissionService.API;
using Microsoft.AspNetCore.Hosting;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);
// Use NLog for logging
builder.Logging.ClearProviders();
builder.Host.UseNLog();

// Use the Startup class
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();
startup.Configure(app);

app.Run();

