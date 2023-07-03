using Application.Middleware;
using Serilog;

namespace WebApi.StartUp;

public static class AppConfig
{
    public static WebApplication ConfigureApp(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(opts =>
            {
                opts.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
        }

        app.UseSerilogRequestLogging();

        app.Use((context, next) =>
        {
            context.Response.Headers.Add("Version", "1.00");
            return next.Invoke();
        });

        app.UseMiddleware<ErrorHandler>();

        app.UseHttpsRedirection();

        app.UseSerilogRequestLogging();

        app.UseRouting();

        app.UseCors("ApiCorsPolicy");

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        return app;
    }
}
