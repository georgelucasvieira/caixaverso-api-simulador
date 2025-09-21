using ApiSimulador.Domain.Entities.Produtos;

namespace ApiSimulador.Infrastructure.Repositories.Produtos;

public class ProdutoRepository
{
    public Task<int> Create(Produto produto)
    {
        throw new NotImplementedException();
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