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
        var dbContextMock = new Mock<ApplicationDbContext>();
        var databaseMock = new Mock<DatabaseFacade>(dbContextMock.Object);

        dbContextMock.Setup(x => x.Database).Returns(databaseMock.Object);
        databaseMock.Setup(x => x.OpenConnectionAsync(default)).Returns(Task.CompletedTask);
        databaseMock.Setup(x => x.CloseConnectionAsync()).Returns(Task.CompletedTask);

        var controller = new HealthController(dbContextMock.Object);

        var result = await controller.GetHealth();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var healthDto = Assert.IsType<HealthCheckDto>(okResult.Value);
        Assert.Equal("Healthy", healthDto.Status);
    }

    [Fact]
    public async Task GetHealth_ReturnsUnhealthy_WhenDatabaseIsDown()
    {
        var dbContextMock = new Mock<ApplicationDbContext>();
        var databaseMock = new Mock<DatabaseFacade>(dbContextMock.Object);

        dbContextMock.Setup(x => x.Database).Returns(databaseMock.Object);
        databaseMock.Setup(x => x.OpenConnectionAsync(default)).ThrowsAsync(new Exception());

        var controller = new HealthController(dbContextMock.Object);

        var result = await controller.GetHealth();

        var statusResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status503ServiceUnavailable, statusResult.StatusCode);
        var healthDto = Assert.IsType<HealthCheckDto>(statusResult.Value);
        Assert.Equal("Unhealthy", healthDto.Status);
    }
}