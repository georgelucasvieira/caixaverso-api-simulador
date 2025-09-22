using Microsoft.OpenApi.Models;
using System.Reflection;

namespace ApiSimulador.Api.Configurations;

public static class OpenAPIConfig
{
    public static void AddOpenAPIConfig(this IServiceCollection services)
    {
        services.AddOpenApi();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "API Simulador Empréstimos",
                Version = "v1.0.0",
                Description = "API para simulação de empréstimos desenvolvida para o Caixaverso" +
                             "Esta API oferece funcionalidades para cadastrar e consultar produtos de crédito, " +
                             "simular empréstimos e consultar histórico de simulações",
                Contact = new OpenApiContact
                {
                    Name = "George Lucas - C158483",
                    Email = "george.carmo@caixa.gov.br"
                }
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlPath))
            {
                c.IncludeXmlComments(xmlPath);
            }

            c.TagActionsBy(api => [api.GroupName ?? api.ActionDescriptor.RouteValues["controller"]]);
            c.DocInclusionPredicate((name, api) => true);

            c.EnableAnnotations();
        });
    }

    public static void UseOpenAPIConfig(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Simulador v1");
                c.RoutePrefix = "swagger";
                c.DocumentTitle = "API Simulador - Documentação";
                c.DefaultModelsExpandDepth(2);
                c.DefaultModelRendering(Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Model);
                c.DisplayRequestDuration();
                c.EnableDeepLinking();
                c.EnableFilter();
                c.ShowExtensions();
            });

            app.MapOpenApi()
                .CacheOutput();
        }
    }
}