using System.Text.Json.Serialization;

namespace ApiSimulador.Api.Models.Common;
public class ApiErrorResponse
{
    [JsonPropertyName("sucesso")]
    public bool Sucesso { get; set; } = false;

    [JsonPropertyName("mensagem")]
    public string? Mensagem { get; set; }

    public ApiErrorResponse(string mensagem)
    {
        Mensagem = mensagem;
    }
}