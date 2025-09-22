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
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProdutoDTO))]
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
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProdutoDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiErrorResponse))]
    public async Task<IActionResult> GetAsync(long id)
    {
        var produtoDto = await _produtoService.GetByIdAsync(id);
        if (produtoDto is null)
            return BadRequest(new ApiErrorResponse("produto não encontrado"));    
        return Ok(produtoDto);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProdutoDTO))]
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
        produtoDto.CoProduto = coProduto;
        return Created($"{Paths.ProdutoV1}/{coProduto}", produtoDto);
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProdutoDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiErrorResponse))]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateProdutoRequest request, long id)
    {
        var produtoDto = new ProdutoDTO
        {
            NomeProduto = request.NomeProduto,
            PrazoMaximoMeses = request.PrazoMaximoMeses,
            TaxaJurosAnual = request.TaxaJurosAnual
        };
        var produtoAtualizadoDto = await _produtoService.UpdateProdutoAsync(id, produtoDto);
        if (produtoAtualizadoDto is null)
            return BadRequest(new ApiErrorResponse("Não foi possível atualizar o produto"));
        return Ok(produtoAtualizadoDto);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProdutoDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiErrorResponse))]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        return Ok("deleted");
    }
}
