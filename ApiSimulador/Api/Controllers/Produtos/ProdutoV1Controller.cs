using ApiSimulador.Api.Models.Common;
using ApiSimulador.Api.Models.Produtos;
using ApiSimulador.Application.Common.Constants;
using ApiSimulador.Application.DTOs.Produtos;
using ApiSimulador.Application.Services.Produtos;
using ApiSimulador.Infrastructure.Repositories.Produtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ApiSimulador.Api.Controllers.Produtos;

[ApiController]
[Route(Paths.ProdutoV1)]
[Produces("application/json")]
public class ProdutoV1Controller : ControllerBase
{
    private readonly ProdutoService _produtoService;
    public ProdutoV1Controller(ProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProdutoDetailResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiErrorResponse))]
    public async Task<IActionResult> FindAllPaginadoAsync(
        [FromBody] CreateProdutoRequest request,
        [FromQuery] int? pagina,
        [FromQuery] int? quantidade)
    {
        return Ok($"find all: pagina {pagina}, quantidade {quantidade}");
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProdutoDetailResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiErrorResponse))]
    public async Task<IActionResult> GetAsync(int id)
    {
        return Ok($"id: {id}");
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProdutoDetailResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiErrorResponse))]
    public async Task<IActionResult> CreateAsync([FromBody] CreateProdutoRequest request)
    {
        var produtoDto = new ProdutoDTO
        {
            NomeProduto = request.NomeProduto,
            TaxaJurosAnual = request.TaxaJurosAnual,
            PrazoMaximoMeses = request.PrazoMaximoMeses,
        };
        var coProduto = await _produtoService.CreateProdutoAsync(produtoDto);

        var response = new ProdutoDetailResponse
        {
            Id = coProduto,
            NomeProduto = produtoDto.NomeProduto,
            PrazoMaximoMeses = produtoDto.PrazoMaximoMeses,
            TaxaJurosAnual = produtoDto.TaxaJurosAnual
        };
        return Created($"{Paths.ProdutoV1}/{coProduto}", response);
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProdutoDetailResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiErrorResponse))]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateProdutoRequest request, int id)
    {
        return Ok($"updated, id: {id}");
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProdutoDetailResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiErrorResponse))]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        return Ok("deleted");
    }
}
