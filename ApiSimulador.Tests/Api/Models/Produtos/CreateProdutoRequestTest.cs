using ApiSimulador.Api.Models.Produtos;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Xunit;

public class CreateProdutoRequestTest
{
    [Fact]
    public void CreateProdutoRequest_ShouldAssignValuesCorrectly()
    {
        var request = new CreateProdutoRequest
        {
            NomeProduto = "Produto Teste",
            PrazoMaximoMeses = 12,
            TaxaJurosAnual = 5.5m
        };

        Assert.Equal("Produto Teste", request.NomeProduto);
        Assert.Equal(12, request.PrazoMaximoMeses);
        Assert.Equal(5.5m, request.TaxaJurosAnual);
    }

    [Fact]
    public void CreateProdutoRequest_ShouldBeValid_WhenAllFieldsAreSet()
    {
        var request = new CreateProdutoRequest
        {
            NomeProduto = "Produto Válido",
            PrazoMaximoMeses = 24,
            TaxaJurosAnual = 6.0m
        };

        var context = new ValidationContext(request);
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(request, context, results, true);

        Assert.True(isValid);
        Assert.Empty(results);
    }

    [Fact]
    public void CreateProdutoRequest_ShouldBeInvalid_WhenRequiredFieldsAreMissing()
    {
        var request = new CreateProdutoRequest();

        var context = new ValidationContext(request);
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(request, context, results, true);

        Assert.False(isValid);
        Assert.Equal(3, results.Count);
    }

    [Fact]
    public void CreateProdutoRequest_ShouldSerializeWithCustomJsonNames()
    {
        var request = new CreateProdutoRequest
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
    public void CreateProdutoRequest_ShouldDeserializeCorrectly()
    {
        var json = "{\"nomeProduto\":\"Produto X\",\"prazoMaximoMeses\":18,\"taxaJurosAnual\":4.75}";
        var request = JsonSerializer.Deserialize<CreateProdutoRequest>(json);

        Assert.NotNull(request);
        Assert.Equal("Produto X", request!.NomeProduto);
        Assert.Equal(18, request.PrazoMaximoMeses);
        Assert.Equal(4.75m, request.TaxaJurosAnual);
    }
}