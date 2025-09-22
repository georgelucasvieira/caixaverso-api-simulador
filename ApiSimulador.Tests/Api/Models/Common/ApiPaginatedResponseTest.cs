using ApiSimulador.Api.Models.Common;
using ApiSimulador.Application.DTOs.Produtos;
using System.Text.Json;
using Xunit;

public class ApiPaginatedResponseTest
{
    [Fact]
    public void ApiPaginatedResponse_ShouldAssignValuesCorrectly()
    {
        var produtos = new List<ProdutoDTO>
        {
            new ProdutoDTO { CoProduto = 1, NomeProduto = "Produto A", PrazoMaximoMeses = 12, TaxaJurosAnual = 5.5m },
            new ProdutoDTO { CoProduto = 2, NomeProduto = "Produto B", PrazoMaximoMeses = 24, TaxaJurosAnual = 6.0m }
        };

        var response = new ApiPaginatedResponse<ProdutoDTO>
        {
            Pagina = 1,
            QtdRegistros = 2,
            QtdRegistrosPagina = 2,
            Registros = produtos
        };

        Assert.Equal(1, response.Pagina);
        Assert.Equal(2, response.QtdRegistros);
        Assert.Equal(2, response.QtdRegistrosPagina);
        Assert.Equal(2, response.Registros.Count);
        Assert.Equal("Produto A", response.Registros[0].NomeProduto);
    }

    [Fact]
    public void ApiPaginatedResponse_ShouldSerializeAndDeserializeCorrectly()
    {
        var produtos = new List<ProdutoDTO>
        {
            new ProdutoDTO { CoProduto = 1, NomeProduto = "Produto A", PrazoMaximoMeses = 12, TaxaJurosAnual = 5.5m }
        };

        var response = new ApiPaginatedResponse<ProdutoDTO>
        {
            Pagina = 1,
            QtdRegistros = 1,
            QtdRegistrosPagina = 1,
            Registros = produtos
        };

        var json = JsonSerializer.Serialize(response);
        var deserialized = JsonSerializer.Deserialize<ApiPaginatedResponse<ProdutoDTO>>(json);

        Assert.NotNull(deserialized);
        Assert.Equal(1, deserialized!.Pagina);
        Assert.Single(deserialized.Registros);
        Assert.Equal("Produto A", deserialized.Registros[0].NomeProduto);
    }
}