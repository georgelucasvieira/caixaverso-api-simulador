using ApiSimulador.Api.Controllers.Produtos;
using ApiSimulador.Api.Models.Common;
using ApiSimulador.Api.Models.Produtos;
using ApiSimulador.Application.DTOs.Produtos;
using ApiSimulador.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class ProdutoV1ControllerTest
{
    private readonly Mock<IProdutoService> _mockService;
    private readonly ProdutoV1Controller _controller;

    public ProdutoV1ControllerTest()
    {
        _mockService = new Mock<IProdutoService>();
        _controller = new ProdutoV1Controller(_mockService.Object);
    }

    [Fact]
    public async Task GetAsync_ShouldReturnProduto_WhenFound()
    {
        var produto = new ProdutoDTO { CoProduto = 1, NomeProduto = "Produto A" };
        _mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(produto);

        var result = await _controller.GetAsync(1);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var dto = Assert.IsType<ProdutoDTO>(okResult.Value);
        Assert.Equal("Produto A", dto.NomeProduto);
    }

    [Fact]
    public async Task GetAsync_ShouldReturnBadRequest_WhenNotFound()
    {
        _mockService.Setup(s => s.GetByIdAsync(99)).ReturnsAsync((ProdutoDTO?)null);

        var result = await _controller.GetAsync(99);

        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        var response = Assert.IsType<ApiDefaultResponse>(badRequest.Value);
        Assert.False(response.Sucesso);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnCreatedResult()
    {
        var request = new CreateProdutoRequest
        {
            NomeProduto = "Novo Produto",
            PrazoMaximoMeses = 12,
            TaxaJurosAnual = 5.5m
        };

        _mockService.Setup(s => s.CreateProdutoAsync(It.IsAny<ProdutoDTO>())).ReturnsAsync(10);

        var result = await _controller.CreateAsync(request);

        var created = Assert.IsType<CreatedResult>(result);
        var dto = Assert.IsType<ProdutoDTO>(created.Value);
        Assert.Equal(10, dto.CoProduto);
        Assert.Equal("Novo Produto", dto.NomeProduto);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnUpdatedProduto_WhenSuccessful()
    {
        var request = new UpdateProdutoRequest
        {
            NomeProduto = "Atualizado",
            PrazoMaximoMeses = 24,
            TaxaJurosAnual = 6.0m
        };

        var updated = new ProdutoDTO
        {
            CoProduto = 1,
            NomeProduto = "Atualizado",
            PrazoMaximoMeses = 24,
            TaxaJurosAnual = 6.0m
        };

        _mockService.Setup(s => s.UpdateProdutoAsync(1, It.IsAny<ProdutoDTO>())).ReturnsAsync(updated);

        var result = await _controller.UpdateAsync(request, 1);

        var ok = Assert.IsType<OkObjectResult>(result);
        var dto = Assert.IsType<ProdutoDTO>(ok.Value);
        Assert.Equal("Atualizado", dto.NomeProduto);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnBadRequest_WhenUpdateFails()
    {
        var request = new UpdateProdutoRequest
        {
            NomeProduto = "Falha",
            PrazoMaximoMeses = 12,
            TaxaJurosAnual = 4.0m
        };

        _mockService.Setup(s => s.UpdateProdutoAsync(1, It.IsAny<ProdutoDTO>())).ReturnsAsync((ProdutoDTO?)null);

        var result = await _controller.UpdateAsync(request, 1);

        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        var response = Assert.IsType<ApiDefaultResponse>(badRequest.Value);
        Assert.False(response.Sucesso);
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnOk_WhenSuccessful()
    {
        _mockService.Setup(s => s.DeleteProdutoAsync(1)).ReturnsAsync(true);

        var result = await _controller.DeleteAsync(1);

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiDefaultResponse>(ok.Value);
        Assert.True(response.Sucesso);
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnBadRequest_WhenFails()
    {
        _mockService.Setup(s => s.DeleteProdutoAsync(1)).ReturnsAsync(false);

        var result = await _controller.DeleteAsync(1);

        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("não foi possível deletar o produto", badRequest.Value);
    }

    [Fact]
    public async Task FindAllPaginadoAsync_ShouldReturnPaginatedResponse()
    {
        var produtos = new List<ProdutoDTO>
        {
            new ProdutoDTO { CoProduto = 1, NomeProduto = "Produto A" },
            new ProdutoDTO { CoProduto = 2, NomeProduto = "Produto B" }
        };

        _mockService.Setup(s => s.CountAllAsync()).ReturnsAsync(2);
        _mockService.Setup(s => s.FindAllPaginatedAsync(1, 10)).ReturnsAsync(produtos);

        var result = await _controller.FindAllPaginadoAsync(null, null);

        var ok = Assert.IsType<OkObjectResult>(result);
        var paginated = Assert.IsType<ApiPaginatedResponse<ProdutoDTO>>(ok.Value);
        Assert.Equal(2, paginated.QtdRegistros);
        Assert.Equal(2, paginated.QtdRegistrosPagina);
        Assert.Equal("Produto A", paginated.Registros[0].NomeProduto);
    }
}