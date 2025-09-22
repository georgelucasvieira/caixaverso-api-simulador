using ApiSimulador.Application.DTOs.Produtos;
using ApiSimulador.Domain.Entities.Produtos;

namespace ApiSimulador.Application.Mappers;

public static class ProdutoMapper
{
    public static ProdutoDTO ToDTO(this Produto produto)
    {
        return new ProdutoDTO
        {
            CoProduto = produto.CoProduto,
            NomeProduto = produto.Nome,
            TaxaJurosAnual = produto.TaxaJurosAnual,
            PrazoMaximoMeses = produto.PrazoMaximoMeses
        };
    }

    public static Produto ToEntity(this ProdutoDTO dto)
    {
        return new Produto
        {
            CoProduto = dto.CoProduto,
            Nome = dto.NomeProduto!,
            TaxaJurosAnual = (decimal) dto.TaxaJurosAnual!,
            PrazoMaximoMeses = (int) dto.PrazoMaximoMeses!
        };
    }

    public static void ToUpdatedEntity(this Produto produto, ProdutoDTO produtoAtualizado)
    {
        if (produtoAtualizado.TaxaJurosAnual is not null && produtoAtualizado.TaxaJurosAnual != 0)
            produto.TaxaJurosAnual = (decimal) produtoAtualizado.TaxaJurosAnual!;
        if (produtoAtualizado.NomeProduto is not null)
            produto.Nome = produtoAtualizado.NomeProduto;
        if(produtoAtualizado.PrazoMaximoMeses is not null && produtoAtualizado.PrazoMaximoMeses != 0)
            produto.PrazoMaximoMeses = (int) produtoAtualizado.PrazoMaximoMeses!;
    }

}
