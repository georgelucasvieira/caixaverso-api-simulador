using ApiSimulador.Application.Interfaces.Repositories;
using ApiSimulador.Application.Interfaces.Services;
using ApiSimulador.Application.Services.Produtos;
using ApiSimulador.Application.Services.Simulacoes;
using ApiSimulador.Infrastructure.Repositories.Produtos;

namespace ApiSimulador.Api.Configurations;

public static class ServicesConfig
{
    public static void AddServicesConfig(this IServiceCollection services)
    {
        services.AddScoped<IProdutoService,ProdutoService>();
        services.AddScoped<SimulacaoService>();
        services.AddScoped<IProdutoRepository, ProdutoRepository>();
    }
}
