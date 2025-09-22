using ApiSimulador.Application.DTOs.Produtos;
using ApiSimulador.Application.Interfaces.Repositories;
using ApiSimulador.Application.Services.Produtos;
using ApiSimulador.Domain.Entities.Produtos;
using ApiSimulador.Infrastructure.Repositories.Produtos;
using Moq;
using Xunit;

public class ProdutoServiceTest
{
    private readonly Mock<IProdutoRepository> _mockRepo;
    private readonly ProdutoService _service;

    public ProdutoServiceTest()
    {
        _mockRepo = new Mock<IProdutoRepository>();
        _service = new ProdutoService(_mockRepo.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnDTO_WhenProdutoExists()
    {
        var produto = new Produto { CoProduto = 1, Nome = "Produto A", TaxaJurosAnual = 5.5m, PrazoMaximoMeses = 12 };
        _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(produto);

        var result = await _service.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal("Produto A", result!.NomeProduto);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenProdutoDoesNotExist()
    {
        _mockRepo.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Produto?)null);

        var result = await _service.GetByIdAsync(99);

        Assert.Null(result);
    }

    [Fact]
    public async Task CreateProdutoAsync_ShouldCallRepositoryAndReturnId()
    {
        var dto = new ProdutoDTO { CoProduto = 0, NomeProduto = "Novo", TaxaJurosAnual = 6.0m, PrazoMaximoMeses = 24 };
        _mockRepo.Setup(r => r.CreateAsync(It.IsAny<Produto>())).ReturnsAsync(10);

        var id = await _service.CreateProdutoAsync(dto);

        Assert.Equal(10, id);
        _mockRepo.Verify(r => r.CreateAsync(It.IsAny<Produto>()), Times.Once);
    }

    [Fact]
    public async Task UpdateProdutoAsync_ShouldReturnUpdatedDTO_WhenProdutoExists()
    {
        var produto = new Produto { CoProduto = 1, Nome = "Antigo", TaxaJurosAnual = 4.0m, PrazoMaximoMeses = 12 };
        var dto = new ProdutoDTO { NomeProduto = "Atualizado", TaxaJurosAnual = 7.0m, PrazoMaximoMeses = 36 };

        _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(produto);
        _mockRepo.Setup(r => r.UpdateAsync(1, produto)).ReturnsAsync(produto);

        var result = await _service.UpdateProdutoAsync(1, dto);

        Assert.NotNull(result);
        Assert.Equal("Atualizado", result!.NomeProduto);
        Assert.Equal(7.0m, result.TaxaJurosAnual);
        Assert.Equal(36, result.PrazoMaximoMeses);
    }

    [Fact]
    public async Task UpdateProdutoAsync_ShouldReturnNull_WhenProdutoNotFound()
    {
        _mockRepo.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Produto?)null);

        var dto = new ProdutoDTO { NomeProduto = "Teste", TaxaJurosAnual = 5.0m, PrazoMaximoMeses = 24 };
        var result = await _service.UpdateProdutoAsync(99, dto);

        Assert.Null(result);
    }

    [Fact]
    public async Task DeleteProdutoAsync_ShouldReturnTrue_WhenDeleted()
    {
        _mockRepo.Setup(r => r.DeleteAsync(1)).ReturnsAsync(true);

        var result = await _service.DeleteProdutoAsync(1);

        Assert.True(result);
    }

    [Fact]
    public async Task FindAllPaginatedAsync_ShouldReturnListOfDTOs()
    {
        var produtos = new List<Produto>
        {
            new Produto { CoProduto = 1, Nome = "Produto A", TaxaJurosAnual = 5.5m, PrazoMaximoMeses = 12 },
            new Produto { CoProduto = 2, Nome = "Produto B", TaxaJurosAnual = 6.0m, PrazoMaximoMeses = 24 }
        };

        _mockRepo.Setup(r => r.FindAllPaginatedAsync(1, 2)).ReturnsAsync(produtos);

        var result = await _service.FindAllPaginatedAsync(1, 2);

        Assert.Equal(2, result.Count);
        Assert.Equal("Produto A", result[0].NomeProduto);
    }

    [Fact]
    public async Task CountAllAsync_ShouldReturnTotal()
    {
        _mockRepo.Setup(r => r.CountAllAsync()).ReturnsAsync(5);

        var count = await _service.CountAllAsync();

        Assert.Equal(5, count);
    }
}