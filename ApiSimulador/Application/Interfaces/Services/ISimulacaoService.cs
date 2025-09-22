using ApiSimulador.Application.DTOs.Produtos;
using ApiSimulador.Application.DTOs.Simulacoes;

namespace ApiSimulador.Application.Interfaces.Services;

public interface ISimulacaoService
{
    SimulacaoDTO SimularTabelaPrice(ProdutoDTO produto, decimal valorSolicitado, int prazoMeses);
}
