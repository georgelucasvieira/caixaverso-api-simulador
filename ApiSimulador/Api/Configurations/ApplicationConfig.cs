using System.Diagnostics.CodeAnalysis;

namespace ApiSimulador.Api.Configurations;

[ExcludeFromCodeCoverage]
public static class ApplicationConfig
{
    public static void AddApplicationConfig(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddCorsConfig();
        builder.Services.AddOpenAPIConfig();
    }

    public static void UseApplicationConfig(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseHttpsRedirection();
        }
        app.UseRouting();
        app.UseCorsConfig();
        app.MapControllers();
        app.UseOpenAPIConfig();
    }
}