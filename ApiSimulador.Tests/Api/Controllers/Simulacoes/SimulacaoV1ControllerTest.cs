using ApiSimulador.Api.Controllers.Simulacoes;
using ApiSimulador.Api.Models.Common;
using ApiSimulador.Api.Models.Simulacoes;
using ApiSimulador.Application.DTOs.Produtos;
using ApiSimulador.Application.DTOs.Simulacoes;
using ApiSimulador.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class SimulacaoV1ControllerTest
{
    private readonly Mock<IProdutoService> _mockProdutoService;
    private readonly Mock<ISimulacaoService> _mockSimulacaoService;
    private readonly SimulacaoV1Controller _controller;

    public SimulacaoV1ControllerTest()
    {
        _mockProdutoService = new Mock<IProdutoService>();
        _mockSimulacaoService = new Mock<ISimulacaoService>();
        _controller = new SimulacaoV1Controller(_mockProdutoService.Object, _mockSimulacaoService.Object);
    }

    [Fact]
    public async Task SimulacaoPrice_ShouldReturnBadRequest_WhenProdutoNotFound()
    {
        var request = new SimulacaoPriceRequest
        {
            IdProduto = 99,
            ValorSolicitado = 10000,
            PrazoMeses = 12
        };

        _mockProdutoService.Setup(s => s.GetByIdAsync(99)).ReturnsAsync((ProdutoDTO?)null);

        var result = await _controller.SimulacaoPrice(request);

        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        var response = Assert.IsType<ApiDefaultResponse>(badRequest.Value);
        Assert.False(response.Sucesso);
        Assert.Equal("Produto não encontrado", response.Mensagem);
    }

    [Fact]
    public async Task SimulacaoPrice_ShouldReturnOk_WhenProdutoExists()
    {
        var produto = new ProdutoDTO
        {
            CoProduto = 1,
            NomeProduto = "Produto Simulável",
            TaxaJurosAnual = 10,
            PrazoMaximoMeses = 24
        };

        var request = new SimulacaoPriceRequest
        {
            IdProduto = 1,
            ValorSolicitado = 5000,
            PrazoMeses = 6
        };

        var simulacao = new SimulacaoDTO
        {
            Produto = produto,
            ValorSolicitado = request.ValorSolicitado,
            PrazoMeses = request.PrazoMeses,
            TaxaJurosEfetivaMensal = 0.007m,
            ParcelaMensal = 860.50m,
            MemoriaCalculos = new List<MemoriaCalculoDTO>()
        };

        _mockProdutoService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(produto);
        _mockSimulacaoService.Setup(s => s.SimularTabelaPrice(produto, request.ValorSolicitado, request.PrazoMeses))
            .Returns(simulacao);

        var result = await _controller.SimulacaoPrice(request);

        var ok = Assert.IsType<OkObjectResult>(result);
        var dto = Assert.IsType<SimulacaoDTO>(ok.Value);
        Assert.Equal(5000, dto.ValorSolicitado);
        Assert.Equal(6, dto.PrazoMeses);
        Assert.Equal(860.50m, dto.ParcelaMensal);
    }
}