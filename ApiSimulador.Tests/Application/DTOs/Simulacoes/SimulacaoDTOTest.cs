using ApiSimulador.Application.DTOs.Produtos;
using ApiSimulador.Application.DTOs.Simulacoes;
using System.Text.Json;
using Xunit;

public class SimulacaoDTOTest
{
    [Fact]
    public void SimulacaoDTO_ShouldAssignPropertiesCorrectly()
    {
        var produto = new ProdutoDTO
        {
            CoProduto = 1,
            NomeProduto = "Produto Teste",
            PrazoMaximoMeses = 12,
            TaxaJurosAnual = 5.5m
        };

        var memoria = new List<MemoriaCalculoDTO>
        {
            new MemoriaCalculoDTO { Mes = 1, SaldoDevedorInicial = 1000, Juros = 50, Amortizacao = 100, SaldoDevedorFinal = 900 },
            new MemoriaCalculoDTO { Mes = 2, SaldoDevedorInicial = 900, Juros = 45, Amortizacao = 105, SaldoDevedorFinal = 795 }
        };

        var simulacao = new SimulacaoDTO
        {
            Produto = produto,
            ValorSolicitado = 1000,
            PrazoMeses = 2,
            TaxaJurosEfetivaMensal = 0.05m,
            ParcelaMensal = 150,
            MemoriaCalculos = memoria
        };

        Assert.Equal(1000, simulacao.ValorSolicitado);
        Assert.Equal(2, simulacao.PrazoMeses);
        Assert.Equal(0.05m, simulacao.TaxaJurosEfetivaMensal);
        Assert.Equal(150, simulacao.ParcelaMensal);
        Assert.Equal("Produto Teste", simulacao.Produto.NomeProduto);
        Assert.Equal(2, simulacao.MemoriaCalculos.Count);
        Assert.Equal(900, simulacao.MemoriaCalculos[0].SaldoDevedorFinal);
    }

    [Fact]
    public void SimulacaoDTO_ShouldSerializeAndDeserializeCorrectly()
    {
        var simulacao = new SimulacaoDTO
        {
            Produto = new ProdutoDTO
            {
                CoProduto = 1,
                NomeProduto = "Produto JSON",
                PrazoMaximoMeses = 12,
                TaxaJurosAnual = 5.5m
            },
            ValorSolicitado = 2000,
            PrazoMeses = 24,
            TaxaJurosEfetivaMensal = 0.04m,
            ParcelaMensal = 120,
            MemoriaCalculos = new List<MemoriaCalculoDTO>
            {
                new MemoriaCalculoDTO { Mes = 1, SaldoDevedorInicial = 2000, Juros = 80, Amortizacao = 40, SaldoDevedorFinal = 1960 }
            }
        };

        var json = JsonSerializer.Serialize(simulacao);
        var deserialized = JsonSerializer.Deserialize<SimulacaoDTO>(json);

        Assert.NotNull(deserialized);
        Assert.Equal(2000, deserialized!.ValorSolicitado);
        Assert.Equal("Produto JSON", deserialized.Produto.NomeProduto);
        Assert.Single(deserialized.MemoriaCalculos);
        Assert.Equal(1960, deserialized.MemoriaCalculos[0].SaldoDevedorFinal);
    }
}