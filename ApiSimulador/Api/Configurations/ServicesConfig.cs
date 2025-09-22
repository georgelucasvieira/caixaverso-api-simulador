using ApiSimulador.Application.Services.Produtos;
using ApiSimulador.Application.Services.Simulacoes;
using ApiSimulador.Infrastructure.Repositories.Produtos;

namespace ApiSimulador.Api.Configurations;

public static class ServicesConfig
{
    public static void AddServicesConfig(this IServiceCollection services)
    {
        services.AddScoped<ProdutoService>();
        services.AddScoped<SimulacaoService>();
        services.AddScoped<ProdutoRepository>();
    }
}
