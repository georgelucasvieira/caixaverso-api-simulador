using ApiSimulador.Application.DTOs.Produtos;
using ApiSimulador.Application.Mappers;
using ApiSimulador.Domain.Entities.Produtos;
using Xunit;

public class ProdutoMapperTest
{
    [Fact]
    public void ToDTO_ShouldMapEntityToDTO()
    {
        var produto = new Produto
        {
            CoProduto = 1,
            Nome = "Produto Teste",
            TaxaJurosAnual = 5.5m,
            PrazoMaximoMeses = 12
        };

        var dto = produto.ToDTO();

        Assert.Equal(produto.CoProduto, dto.CoProduto);
        Assert.Equal(produto.Nome, dto.NomeProduto);
        Assert.Equal(produto.TaxaJurosAnual, dto.TaxaJurosAnual);
        Assert.Equal(produto.PrazoMaximoMeses, dto.PrazoMaximoMeses);
    }

    [Fact]
    public void ToEntity_ShouldMapDTOToEntity()
    {
        var dto = new ProdutoDTO
        {
            CoProduto = 2,
            NomeProduto = "Produto DTO",
            TaxaJurosAnual = 6.0m,
            PrazoMaximoMeses = 24
        };

        var entity = dto.ToEntity();

        Assert.Equal(dto.CoProduto, entity.CoProduto);
        Assert.Equal(dto.NomeProduto, entity.Nome);
        Assert.Equal(dto.TaxaJurosAnual, entity.TaxaJurosAnual);
        Assert.Equal(dto.PrazoMaximoMeses, entity.PrazoMaximoMeses);
    }

    [Fact]
    public void ToUpdatedEntity_ShouldUpdateOnlyNonNullAndNonZeroFields()
    {
        var produto = new Produto
        {
            CoProduto = 3,
            Nome = "Original",
            TaxaJurosAnual = 4.0m,
            PrazoMaximoMeses = 36
        };

        var dto = new ProdutoDTO
        {
            NomeProduto = "Atualizado",
            TaxaJurosAnual = 0, 
            PrazoMaximoMeses = null
        };

        produto.ToUpdatedEntity(dto);

        Assert.Equal("Atualizado", produto.Nome);
        Assert.Equal(4.0m, produto.TaxaJurosAnual);
        Assert.Equal(36, produto.PrazoMaximoMeses);
    }

    [Fact]
    public void ToUpdatedEntity_ShouldUpdateAllValidFields()
    {
        var produto = new Produto
        {
            CoProduto = 4,
            Nome = "Original",
            TaxaJurosAnual = 4.0m,
            PrazoMaximoMeses = 36
        };

        var dto = new ProdutoDTO
        {
            NomeProduto = "Novo Nome",
            TaxaJurosAnual = 7.25m,
            PrazoMaximoMeses = 48
        };

        produto.ToUpdatedEntity(dto);

        Assert.Equal("Novo Nome", produto.Nome);
        Assert.Equal(7.25m, produto.TaxaJurosAnual);
        Assert.Equal(48, produto.PrazoMaximoMeses);
    }
}