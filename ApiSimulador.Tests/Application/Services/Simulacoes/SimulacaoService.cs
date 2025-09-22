using ApiSimulador.Application.DTOs.Produtos;
using ApiSimulador.Application.Services.Simulacoes;
using Xunit;

public class SimulacaoServiceTest
{
    private readonly SimulacaoService _service;

    public SimulacaoServiceTest()
    {
        _service = new SimulacaoService();
    }

    [Fact]
    public void SimularTabelaPrice_ShouldCalculateCorrectly()
    {
        var produto = new ProdutoDTO
        {
            CoProduto = 1,
            NomeProduto = "Produto Teste",
            TaxaJurosAnual = 12.0m,
            PrazoMaximoMeses = 60
        };

        var valorSolicitado = 10000m;
        var prazoMeses = 12;

        var simulacao = _service.SimularTabelaPrice(produto, valorSolicitado, prazoMeses);

        Assert.NotNull(simulacao);
        Assert.Equal(valorSolicitado, simulacao.ValorSolicitado);
        Assert.Equal(prazoMeses, simulacao.PrazoMeses);
        Assert.True(simulacao.TaxaJurosEfetivaMensal > 0);
        Assert.True(simulacao.ParcelaMensal > 0);
        Assert.Equal(prazoMeses, simulacao.MemoriaCalculos.Count);
    }

    [Fact]
    public void SimularTabelaPrice_ShouldZeroSaldoFinalInLastMonth()
    {
        var produto = new ProdutoDTO
        {
            CoProduto = 2,
            NomeProduto = "Produto Teste",
            TaxaJurosAnual = 10.0m,
            PrazoMaximoMeses = 24
        };

        var simulacao = _service.SimularTabelaPrice(produto, 5000m, 6);

        var ultimoMes = simulacao.MemoriaCalculos.Last();
        Assert.Equal(0, ultimoMes.SaldoDevedorFinal);
    }

    [Fact]
    public void SimularTabelaPrice_ShouldHandleSmallValues()
    {
        var produto = new ProdutoDTO
        {
            CoProduto = 3,
            NomeProduto = "Produto Pequeno",
            TaxaJurosAnual = 5.0m,
            PrazoMaximoMeses = 12
        };

        var simulacao = _service.SimularTabelaPrice(produto, 100m, 3);

        Assert.Equal(3, simulacao.MemoriaCalculos.Count);
        Assert.All(simulacao.MemoriaCalculos, m =>
        {
            Assert.True(m.Juros >= 0);
            Assert.True(m.Amortizacao >= 0);
        });
    }
}