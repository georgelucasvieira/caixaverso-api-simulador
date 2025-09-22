using ApiSimulador.Application.DTOs.Simulacoes;
using System.Text.Json;
using Xunit;

public class MemoriaCalculoDTOTests
{
    [Fact]
    public void MemoriaCalculoDTO_ShouldAssignPropertiesCorrectly()
    {
        var memoria = new MemoriaCalculoDTO
        {
            Mes = 1,
            SaldoDevedorInicial = 1000,
            Juros = 50,
            Amortizacao = 100,
            SaldoDevedorFinal = 900
        };

        Assert.Equal(1, memoria.Mes);
        Assert.Equal(1000, memoria.SaldoDevedorInicial);
        Assert.Equal(50, memoria.Juros);
        Assert.Equal(100, memoria.Amortizacao);
        Assert.Equal(900, memoria.SaldoDevedorFinal);
    }

    [Fact]
    public void MemoriaCalculoDTO_ShouldSerializeAndDeserializeCorrectly()
    {
        var memoria = new MemoriaCalculoDTO
        {
            Mes = 2,
            SaldoDevedorInicial = 900,
            Juros = 45,
            Amortizacao = 105,
            SaldoDevedorFinal = 795
        };

        var json = JsonSerializer.Serialize(memoria);
        var deserialized = JsonSerializer.Deserialize<MemoriaCalculoDTO>(json);

        Assert.NotNull(deserialized);
        Assert.Equal(2, deserialized!.Mes);
        Assert.Equal(900, deserialized.SaldoDevedorInicial);
        Assert.Equal(45, deserialized.Juros);
        Assert.Equal(105, deserialized.Amortizacao);
        Assert.Equal(795, deserialized.SaldoDevedorFinal);
    }
}