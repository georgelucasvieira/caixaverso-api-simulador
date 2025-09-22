using ApiSimulador.Application.DTOs.Produtos;

namespace ApiSimulador.Application.Interfaces.Services;

public interface IProdutoService
{
    Task<ProdutoDTO?> GetByIdAsync(long id);
    Task<List<ProdutoDTO>> FindAllPaginatedAsync(int pagina, int quantidade);
    Task<long> CountAllAsync();
    Task<long> CreateProdutoAsync(ProdutoDTO produtoDto);
    Task<ProdutoDTO?> UpdateProdutoAsync(long id, ProdutoDTO produtoDto);
    Task<bool> DeleteProdutoAsync(long id);
}
