using Infrastructure;
using Application;
using Persistence;

namespace WebApi.StartUp;

public static class StandardServicesConfig
{
    public static void RegisterStandardServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddApplicationServices(builder.Configuration);
        builder.Services.AddInfrastructureServices(builder.Configuration);
        builder.Services.AddPersistenceServices(builder.Configuration);
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddCors(opts => opts.AddPolicy("ApiCorsPolicy", builder =>
        {
            builder.WithOrigins().WithMethods("GET", "PUT", "DELETE", "POST").AllowAnyHeader().AllowCredentials().WithExposedHeaders("Version");
        }));
    }
}
