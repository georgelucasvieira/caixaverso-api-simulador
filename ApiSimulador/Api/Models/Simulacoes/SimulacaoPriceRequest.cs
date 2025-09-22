using System.Text.Json.Serialization;

namespace ApiSimulador.Api.Models.Simulacoes;

public class SimulacaoPriceRequest
{
    [JsonPropertyName("idProduto")]
    public long IdProduto { get; set; }

    [JsonPropertyName("valorSolicitado")]
    public decimal ValorSolicitado { get; set; }

    [JsonPropertyName("prazoMeses")]
    public int PrazoMeses { get; set; }
}
