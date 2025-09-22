using ApiSimulador.Api.Models.Produtos;
using System.Text.Json;
using Xunit;

public class UpdateProdutoRequestTest
{
    [Fact]
    public void UpdateProdutoRequest_ShouldAssignValuesCorrectly()
    {
        var request = new UpdateProdutoRequest
        {
            NomeProduto = "Produto Atualizado",
            PrazoMaximoMeses = 24,
            TaxaJurosAnual = 6.5m
        };

        Assert.Equal("Produto Atualizado", request.NomeProduto);
        Assert.Equal(24, request.PrazoMaximoMeses);
        Assert.Equal(6.5m, request.TaxaJurosAnual);
    }

    [Fact]
    public void UpdateProdutoRequest_ShouldAllowNullValues()
    {
        var request = new UpdateProdutoRequest
        {
            NomeProduto = null,
            PrazoMaximoMeses = null,
            TaxaJurosAnual = null
        };

        Assert.Null(request.NomeProduto);
        Assert.Null(request.PrazoMaximoMeses);
        Assert.Null(request.TaxaJurosAnual);
    }

    [Fact]
    public void UpdateProdutoRequest_ShouldSerializeWithCustomJsonNames()
    {
        var request = new UpdateProdutoRequest
        {
            NomeProduto = "Produto JSON",
            PrazoMaximoMeses = 36,
            TaxaJurosAnual = 7.25m
        };

        var json = JsonSerializer.Serialize(request);
        Assert.Contains("\"nomeProduto\":\"Produto JSON\"", json);
        Assert.Contains("\"prazoMaximoMeses\":36", json);
        Assert.Contains("\"taxaJurosAnual\":7.25", json);
    }

    [Fact]
    public void UpdateProdutoRequest_ShouldDeserializeCorrectly()
    {
        var json = "{\"nomeProduto\":\"Produto X\",\"prazoMaximoMeses\":18,\"taxaJurosAnual\":4.75}";
        var request = JsonSerializer.Deserialize<UpdateProdutoRequest>(json);

        Assert.NotNull(request);
        Assert.Equal("Produto X", request!.NomeProduto);
        Assert.Equal(18, request.PrazoMaximoMeses);
        Assert.Equal(4.75m, request.TaxaJurosAnual);
    }

    [Fact]
    public void UpdateProdutoRequest_ShouldDeserializeWithMissingFields()
    {
        var json = "{}";
        var request = JsonSerializer.Deserialize<UpdateProdutoRequest>(json);

        Assert.NotNull(request);
        Assert.Null(request!.NomeProduto);
        Assert.Null(request.PrazoMaximoMeses);
        Assert.Null(request.TaxaJurosAnual);
    }
}