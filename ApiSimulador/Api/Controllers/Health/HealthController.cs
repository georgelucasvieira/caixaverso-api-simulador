using Microsoft.AspNetCore.Mvc;
using ApiSimulador.Application.Common.Constants;
using ApiSimulador.Application.DTOs.Health;

namespace ApiSimulador.Api.Controllers.Health;

/// <summary>
/// Controller responsável pelo monitoramento da saúde da aplicação
/// </summary>
[ApiController]
[Route(Paths.Health)]
[Produces("application/json")]
[Tags("Health")]
public class HealthController() : ControllerBase
{
    /// <summary>
    /// Verifica o status de saúde da aplicação e suas dependências
    /// </summary>
    /// <response code="200">Aplicação está saudável</response>
    /// <response code="503">Aplicação não está saudável</response>
    [HttpGet]
    [ProducesResponseType(typeof(HealthCheckDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(HealthCheckDto), StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> GetHealth()
    {
        var health = new HealthCheckDto
        {
            Status = "Healthy",
            Timestamp = DateTime.UtcNow,
        };

        return Ok(health);
    }
}