using ApiSimulador.Api.Models.Simulacoes;
using System.Text.Json;
using Xunit;

public class SimulacaoPriceRequestTest
{
    [Fact]
    public void SimulacaoPriceRequest_ShouldAssignValuesCorrectly()
    {
        var request = new SimulacaoPriceRequest
        {
            IdProduto = 10,
            ValorSolicitado = 5000.75m,
            PrazoMeses = 36
        };

        Assert.Equal(10, request.IdProduto);
        Assert.Equal(5000.75m, request.ValorSolicitado);
        Assert.Equal(36, request.PrazoMeses);
    }

    [Fact]
    public void SimulacaoPriceRequest_ShouldSerializeWithCustomJsonNames()
    {
        var request = new SimulacaoPriceRequest
        {
            IdProduto = 5,
            ValorSolicitado = 10000,
            PrazoMeses = 48
        };

        var json = JsonSerializer.Serialize(request);
        Assert.Contains("\"idProduto\":5", json);
        Assert.Contains("\"valorSolicitado\":10000", json);
        Assert.Contains("\"prazoMeses\":48", json);
    }

    [Fact]
    public void SimulacaoPriceRequest_ShouldDeserializeCorrectly()
    {
        var json = "{\"idProduto\":3,\"valorSolicitado\":7500.50,\"prazoMeses\":24}";
        var request = JsonSerializer.Deserialize<SimulacaoPriceRequest>(json);

        Assert.NotNull(request);
        Assert.Equal(3, request!.IdProduto);
        Assert.Equal(7500.50m, request.ValorSolicitado);
        Assert.Equal(24, request.PrazoMeses);
    }

    [Fact]
    public void SimulacaoPriceRequest_ShouldAllowDefaultValues()
    {
        var request = new SimulacaoPriceRequest(); // todos os valores default

        Assert.Equal(0, request.IdProduto);
        Assert.Equal(0, request.ValorSolicitado);
        Assert.Equal(0, request.PrazoMeses);
    }
}