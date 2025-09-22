using ApiSimulador.Domain.Entities.Produtos;
using ApiSimulador.Infrastructure.Data.Contexts;
using ApiSimulador.Infrastructure.Repositories.Produtos;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class ProdutoRepositoryTests
{
    private ApplicationDbContext GetDbContext()
    { 
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite("DataSource=:memory:")
            .Options;

        var context = new ApplicationDbContext(options);
        context.Database.OpenConnection();
        context.Database.EnsureCreated();
        return context;
    }

    [Fact]
    public async Task CreateAsync_ShouldAddProduto()
    {
        var context = GetDbContext();
        var repository = new ProdutoRepository(context);

        var produto = new Produto
        {
            Nome = "Produto Teste",
            TaxaJurosAnual = 5.5m,
            PrazoMaximoMeses = 12
        };

        var id = await repository.CreateAsync(produto);

        var produtoCriado = await context.Produtos.FindAsync(id);
        Assert.NotNull(produtoCriado);
        Assert.Equal("Produto Teste", produtoCriado.Nome);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnProduto()
    {
        var context = GetDbContext();
        var produto = new Produto { Nome = "Produto A", TaxaJurosAnual = 3.2m, PrazoMaximoMeses = 24 };
        context.Produtos.Add(produto);
        await context.SaveChangesAsync();

        var repository = new ProdutoRepository(context);
        var result = await repository.GetByIdAsync(produto.CoProduto);

        Assert.NotNull(result);
        Assert.Equal(produto.Nome, result!.Nome);
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveProduto()
    {
        var context = GetDbContext();
        var produto = new Produto { Nome = "Produto B", TaxaJurosAnual = 4.0m, PrazoMaximoMeses = 36 };
        context.Produtos.Add(produto);
        await context.SaveChangesAsync();

        var repository = new ProdutoRepository(context);
        var deleted = await repository.DeleteAsync(produto.CoProduto);

        Assert.True(deleted);
        Assert.Null(await repository.GetByIdAsync(produto.CoProduto));
    }

    [Fact]
    public async Task FindAllPaginatedAsync_ShouldReturnPaginatedList()
    {
        var context = GetDbContext();
        for (int i = 1; i <= 10; i++)
        {
            context.Produtos.Add(new Produto { Nome = $"Produto {i}", TaxaJurosAnual = i, PrazoMaximoMeses = i * 10 });
        }
        await context.SaveChangesAsync();

        var repository = new ProdutoRepository(context);
        var result = await repository.FindAllPaginatedAsync(2, 3); // Página 2, 3 itens por página

        Assert.Equal(3, result.Count);
        Assert.Equal("Produto 4", result[0].Nome);
    }

    [Fact]
    public async Task CountAllAsync_ShouldReturnTotalCount()
    {
        var context = GetDbContext();
        context.Produtos.AddRange(
            new Produto { Nome = "Produto 1", TaxaJurosAnual = 1, PrazoMaximoMeses = 10 },
            new Produto { Nome = "Produto 2", TaxaJurosAnual = 2, PrazoMaximoMeses = 20 }
        );
        await context.SaveChangesAsync();

        var repository = new ProdutoRepository(context);
        var count = await repository.CountAllAsync();

        Assert.Equal(2, count);
    }

    [Fact]
    public async Task UpdateAsync_ShouldModifyProduto()
    {
        var context = GetDbContext();
        var produto = new Produto { Nome = "Original", TaxaJurosAnual = 1.0m, PrazoMaximoMeses = 10 };
        context.Produtos.Add(produto);
        await context.SaveChangesAsync();

        var repository = new ProdutoRepository(context);
        var atualizado = new Produto
        {
            Nome = "Atualizado",
            TaxaJurosAnual = 2.5m,
            PrazoMaximoMeses = 20
        };

        var result = await repository.UpdateAsync(produto.CoProduto, atualizado);

        Assert.NotNull(result);
        Assert.Equal("Atualizado", result!.Nome);
    }
}
