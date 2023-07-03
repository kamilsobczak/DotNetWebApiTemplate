using Serilog;
using WebApi.StartUp;

Environment.SetEnvironmentVariable("APP_BASE_DIRECTORY", AppContext.BaseDirectory);

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("%APP_BASE_DIRECTORY%/configuration.json", optional: true);

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

// Add services to the container.
builder.RegisterStandardServices();

builder.RegisterSwagger();

builder.AddAuthServices();

try
{
    var app = builder.Build();

    Log.Information("Application is starting up");

    app.ConfigureApp();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Could not start up application");
}
finally
{
    Log.Information("Application is closing up");
    Log.CloseAndFlush();
}