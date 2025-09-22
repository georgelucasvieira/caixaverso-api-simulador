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
            TaxaJurosAnual = dto.TaxaJurosAnual,
            PrazoMaximoMeses = dto.PrazoMaximoMeses
        };
    }
}
