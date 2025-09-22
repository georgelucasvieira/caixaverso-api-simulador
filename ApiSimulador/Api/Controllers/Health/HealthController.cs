using ApiSimulador.Application.Common.Constants;
using ApiSimulador.Application.DTOs.Health;
using ApiSimulador.Infrastructure.Data.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiSimulador.Api.Controllers.Health;

/// <summary>
/// Controller responsável pelo monitoramento da saúde da aplicação
/// </summary>
[ApiController]
[Route(Paths.Health)]
[Produces("application/json")]
[Tags("Health")]
public class HealthController : ControllerBase
{
    private readonly ApplicationDbContext _applicationDbContext;

    public HealthController(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

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
        bool isDbUp = await CheckDatabaseConnection(_applicationDbContext);
        if (!isDbUp)
        {
            var unhealthy = new HealthCheckDto
            {
                Status = "Unhealthy",
                Timestamp = DateTime.UtcNow,
            };
            return StatusCode(StatusCodes.Status503ServiceUnavailable, unhealthy);
        }

        var healthy = new HealthCheckDto
        {
            Status = "Healthy",
            Timestamp = DateTime.UtcNow,
        };

        return Ok(healthy);
    }

    private static async Task<bool> CheckDatabaseConnection(DbContext context)
    {
        try
        {
            await context.Database.OpenConnectionAsync();
            await context.Database.CloseConnectionAsync();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}