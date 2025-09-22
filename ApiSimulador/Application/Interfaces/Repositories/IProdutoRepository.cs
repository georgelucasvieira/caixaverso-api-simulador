using ApiSimulador.Domain.Entities.Produtos;

namespace ApiSimulador.Application.Interfaces.Repositories
{

    public interface IProdutoRepository
    {
        Task<Produto?> GetByIdAsync(long id);
        Task<List<Produto>> FindAllPaginatedAsync(int pagina, int quantidade);
        Task<long> CountAllAsync();
        Task<long> CreateAsync(Produto produto);
        Task<Produto?> UpdateAsync(long id, Produto produtoAtualizado);
        Task<bool> DeleteAsync(long id);
    }

}
