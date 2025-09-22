using ApiSimulador.Application.DTOs.Produtos;
using ApiSimulador.Application.Mappers;
using ApiSimulador.Infrastructure.Repositories.Produtos;

namespace ApiSimulador.Application.Services.Produtos;

public class ProdutoService
{
    private readonly ProdutoRepository _produtoRepository;

    public ProdutoService(ProdutoRepository produtoRepository) 
    {
        _produtoRepository = produtoRepository;
    }

    public async Task<ProdutoDTO?> GetByIdAsync(long id)
    {
        var produto = await _produtoRepository.GetByIdAsync(id);
        if(produto is null) return null;
        return produto.ToDTO();
    }

    public async Task<long> CreateProdutoAsync(ProdutoDTO produtoDto)
    {
        return await _produtoRepository.CreateAsync(produtoDto.ToEntity());
    }

    public async Task<ProdutoDTO?> UpdateProdutoAsync(long id, ProdutoDTO produtoDto)
    {
        var produto = await _produtoRepository.GetByIdAsync(id);
        if (produto is null) return null;
        produto.ToUpdatedEntity(produtoDto);
        var produtoAtualizado = await _produtoRepository.UpdateAsync(id, produto);
        if (produtoAtualizado is null) return null;
        return produtoAtualizado.ToDTO();
    }
}
