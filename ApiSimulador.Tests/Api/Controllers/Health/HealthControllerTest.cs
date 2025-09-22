using ApiSimulador.Api.Controllers.Health;
using ApiSimulador.Application.DTOs.Health;
using ApiSimulador.Infrastructure.Data.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Moq;
using System.Threading.Tasks;
using Xunit;

public class HealthControllerTests
{
    [Fact]
    public async Task GetHealth_ReturnsHealthy_WhenDatabaseIsUp()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite("DataSource=:memory:")
            .Options;

        var context = new ApplicationDbContext(options);
        context.Database.OpenConnection();
        context.Database.EnsureCreated();

        var controller = new HealthController(context);

        var result = await controller.GetHealth();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var healthDto = Assert.IsType<HealthCheckDto>(okResult.Value);
        Assert.Equal("Healthy", healthDto.Status);
    }
}