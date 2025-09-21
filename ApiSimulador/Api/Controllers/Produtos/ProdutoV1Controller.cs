using ApiSimulador.Api.Models.Common;
using ApiSimulador.Api.Models.Produtos;
using ApiSimulador.Application.Common.Constants;
using Microsoft.AspNetCore.Mvc;

namespace ApiSimulador.Api.Controllers.Produtos;

[ApiController]
[Route(Paths.ProdutoV1)]
[Produces("application/json")]
public class ProdutoV1Controller : ControllerBase
{
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
        return Ok("created");
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
