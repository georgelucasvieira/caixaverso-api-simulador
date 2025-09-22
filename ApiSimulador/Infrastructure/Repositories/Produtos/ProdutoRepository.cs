using ApiSimulador.Application.DTOs.Produtos;
using ApiSimulador.Domain.Entities.Produtos;
using ApiSimulador.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ApiSimulador.Infrastructure.Repositories.Produtos;

public class ProdutoRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<Produto> _dbSet;

    public ProdutoRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Produtos;
    }

    public async Task<Produto?> GetByIdAsync(long id)
    {
        return await _dbSet.FirstOrDefaultAsync(p => p.CoProduto == id);
    }

    public async Task<long> CreateAsync(Produto produto)
    {
        await _dbSet.AddAsync(produto);
        await _dbContext.SaveChangesAsync();
        return produto.CoProduto;
    }

    public Task<int> Delete(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Produto>> FindAllPaginatedAsync(int pagina, int quantidade)
    {
        return await _dbSet
            .Skip((pagina - 1) * quantidade)
            .Take(quantidade)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<long> CountAllAsync()
    {
        return await _dbSet.CountAsync();
    }

    public async Task<Produto?> UpdateAsync(long id, Produto produtoAtualizado)
    {
        var rowsAffected = await _dbSet.Where(produto => produto.CoProduto == id)
            .ExecuteUpdateAsync(updates =>
                updates.SetProperty(produto => produto.TaxaJurosAnual, produtoAtualizado.TaxaJurosAnual)
                       .SetProperty(produto => produto.PrazoMaximoMeses, produtoAtualizado.PrazoMaximoMeses)
                       .SetProperty(produto => produto.Nome, produtoAtualizado.Nome));

        return rowsAffected != 0 ? produtoAtualizado : null;
    }
}