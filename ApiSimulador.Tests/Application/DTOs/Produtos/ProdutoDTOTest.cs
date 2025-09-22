using ApiSimulador.Application.DTOs.Produtos;
using System.Text.Json;
using Xunit;

public class ProdutoDTOTest
{
    [Fact]
    public void ProdutoDTO_ShouldAssignPropertiesCorrectly()
    {
        var dto = new ProdutoDTO
        {
            CoProduto = 1,
            NomeProduto = "Produto Teste",
            PrazoMaximoMeses = 12,
            TaxaJurosAnual = 5.5m
        };

        Assert.Equal(1, dto.CoProduto);
        Assert.Equal("Produto Teste", dto.NomeProduto);
        Assert.Equal(12, dto.PrazoMaximoMeses);
        Assert.Equal(5.5m, dto.TaxaJurosAnual);
    }

    [Fact]
    public void ProdutoDTO_ShouldSerializeToJsonCorrectly()
    {
        var dto = new ProdutoDTO
        {
            CoProduto = 2,
            NomeProduto = "Produto JSON",
            PrazoMaximoMeses = 24,
            TaxaJurosAnual = 3.75m
        };

        var json = JsonSerializer.Serialize(dto);
        Assert.Contains("\"CoProduto\":2", json);
        Assert.Contains("\"NomeProduto\":\"Produto JSON\"", json);
        Assert.Contains("\"PrazoMaximoMeses\":24", json);
        Assert.Contains("\"TaxaJurosAnual\":3.75", json);
    }

    [Fact]
    public void ProdutoDTO_ShouldDeserializeFromJsonCorrectly()
    {
        var json = "{\"CoProduto\":3,\"NomeProduto\":\"Produto Deserializado\",\"PrazoMaximoMeses\":36,\"TaxaJurosAnual\":4.25}";
        var dto = JsonSerializer.Deserialize<ProdutoDTO>(json);

        Assert.NotNull(dto);
        Assert.Equal(3, dto!.CoProduto);
        Assert.Equal("Produto Deserializado", dto.NomeProduto);
        Assert.Equal(36, dto.PrazoMaximoMeses);
        Assert.Equal(4.25m, dto.TaxaJurosAnual);
    }
}