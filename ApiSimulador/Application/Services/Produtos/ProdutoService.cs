using ApiSimulador.Application.DTOs.Produtos;
using ApiSimulador.Application.Mappers;
using ApiSimulador.Infrastructure.Repositories.Produtos;

namespace ApiSimulador.Application.Services.Produtos;

public class ProdutoService
{
    private readonly ProdutoRepository _produtosRepository;

    public ProdutoService(ProdutoRepository produtoRepository) 
    {
        _produtosRepository = produtoRepository;
    }

    public async Task<long> CreateProdutoAsync(ProdutoDTO produtoDto)
    {
        return await _produtosRepository.CreateAsync(produtoDto.ToEntity());
    }
}
