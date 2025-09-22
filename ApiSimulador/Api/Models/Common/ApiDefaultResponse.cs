using System.Text.Json.Serialization;

namespace ApiSimulador.Api.Models.Common;
public class ApiDefaultResponse
{
    [JsonPropertyName("sucesso")]
    public bool Sucesso { get; set; }

    [JsonPropertyName("mensagem")]
    public string? Mensagem { get; set; }

    public ApiDefaultResponse(bool sucesso, string mensagem)
    {
        Mensagem = mensagem;
        Sucesso = sucesso;
    }
}