namespace ApiSimulador.Application.DTOs.Health;

public class HealthCheckDto
{
    public string Status { get; set; } = "Healthy";
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}