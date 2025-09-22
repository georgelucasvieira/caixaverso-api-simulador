using ApiSimulador.Api.Models.Common;
using ApiSimulador.Api.Models.Simulacoes;
using ApiSimulador.Application.Common.Constants;
using ApiSimulador.Application.Interfaces.Services;
using ApiSimulador.Application.Services.Produtos;
using ApiSimulador.Application.Services.Simulacoes;
using Microsoft.AspNetCore.Mvc;

namespace ApiSimulador.Api.Controllers.Simulacoes;
[ApiController]
[Route(Paths.SimulacaoV1)]
[Produces("application/json")]
public class SimulacaoV1Controller: ControllerBase
{
    private readonly IProdutoService _produtoService;
    private readonly SimulacaoService _simulacaoService;

    public SimulacaoV1Controller(IProdutoService produtoService, SimulacaoService simulacaoService)
    {
        _produtoService = produtoService;
        _simulacaoService = simulacaoService;
    }

    [HttpPost]
    public async Task<IActionResult> SimulacaoPrice([FromBody] SimulacaoPriceRequest request)
    {
        var produto = await _produtoService.GetByIdAsync(request.IdProduto);
        if (produto is null)
            return BadRequest(new ApiDefaultResponse(false, "Produto não encontrado"));
        var simulacao = _simulacaoService.SimularTabelaPrice(produto!, request.ValorSolicitado, request.PrazoMeses);
        return Ok(simulacao);
    } 
}
