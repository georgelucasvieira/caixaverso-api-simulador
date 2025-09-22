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

    public Task<IEnumerable<Produto>> FindAll()
    {
        throw new NotImplementedException();
    }

    public Task<Produto> Update(Produto produto)
    {
        throw new NotImplementedException();
    }
}