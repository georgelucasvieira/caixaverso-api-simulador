using System.Diagnostics.CodeAnalysis;

namespace ApiSimulador.Api.Configurations;

[ExcludeFromCodeCoverage]
public static class CorsConfig
{
    private static readonly string CorsPolicyName = "CorsPolicy";
    private static readonly string[] AllowedHeaders = ["Content-Type", "Authorization"];
    private static readonly string[] AllowedMethods = ["GET", "POST", "PUT", "PATCH", "DELETE", "OPTIONS"];

    public static void AddCorsConfig(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(CorsPolicyName, builder =>
            {
                var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
                var allowedOrigins = configuration.GetSection("AllowedOrigins").Get<string[]>()
                    ?? ["http://localhost:3000"];

                ConfigureOrigins(builder, allowedOrigins);
                ConfigureHeaders(builder);
                ConfigureMethods(builder);
            });
        });
    }

    public static void UseCorsConfig(this IApplicationBuilder app)
    {
        app.UseCors(CorsPolicyName);
    }

    private static void ConfigureOrigins(Microsoft.AspNetCore.Cors.Infrastructure.CorsPolicyBuilder builder, string[] allowedOrigins)
    {
        var hasWildcard = allowedOrigins.Contains("*");

        if (hasWildcard)
        {
            builder.AllowAnyOrigin();
            return;
        }

        builder.WithOrigins(allowedOrigins);
    }

    private static void ConfigureHeaders(Microsoft.AspNetCore.Cors.Infrastructure.CorsPolicyBuilder builder)
    {
        builder.WithHeaders(AllowedHeaders)
               .WithExposedHeaders(AllowedHeaders);
    }

    private static void ConfigureMethods(Microsoft.AspNetCore.Cors.Infrastructure.CorsPolicyBuilder builder)
    {
        builder.WithMethods(AllowedMethods);
    }
}