using ApiSimulador.Application.DTOs.Health;
using System.Text.Json;
using Xunit;

public class HealthCheckDtoTest
{
    [Fact]
    public void HealthCheckDto_ShouldHaveDefaultValues()
    {
        var dto = new HealthCheckDto();

        Assert.Equal("Healthy", dto.Status);
        Assert.True((DateTime.UtcNow - dto.Timestamp).TotalSeconds < 5);
    }

    [Fact]
    public void HealthCheckDto_ShouldAllowCustomValues()
    {
        var customTime = new DateTime(2023, 1, 1, 12, 0, 0, DateTimeKind.Utc);
        var dto = new HealthCheckDto
        {
            Status = "Unhealthy",
            Timestamp = customTime
        };

        Assert.Equal("Unhealthy", dto.Status);
        Assert.Equal(customTime, dto.Timestamp);
    }

    [Fact]
    public void HealthCheckDto_ShouldSerializeAndDeserializeCorrectly()
    {
        var dto = new HealthCheckDto
        {
            Status = "Degraded",
            Timestamp = new DateTime(2024, 5, 10, 14, 30, 0, DateTimeKind.Utc)
        };

        var json = JsonSerializer.Serialize(dto);
        var deserialized = JsonSerializer.Deserialize<HealthCheckDto>(json);

        Assert.NotNull(deserialized);
        Assert.Equal("Degraded", deserialized!.Status);
        Assert.Equal(dto.Timestamp, deserialized.Timestamp);
    }
}
