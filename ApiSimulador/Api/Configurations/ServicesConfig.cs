using ApiSimulador.Application.Services.Produtos;
using ApiSimulador.Infrastructure.Repositories.Produtos;

namespace ApiSimulador.Api.Configurations;

public static class ServicesConfig
{
    public static void AddServicesConfig(this IServiceCollection services)
    {
        services.AddScoped<ProdutoService>();
        services.AddScoped<ProdutoRepository>();
    }
}
