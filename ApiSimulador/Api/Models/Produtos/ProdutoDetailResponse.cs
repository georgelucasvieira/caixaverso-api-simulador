using System.Text.Json.Serialization;

namespace ApiSimulador.Api.Models.Produtos;
public class ProdutoDetailResponse
{
    [JsonPropertyName("idProduto")]
    public long Id { get; set; }

    [JsonPropertyName("nomeProduto")]
    public string? NomeProduto { get; set; }

    [JsonPropertyName("prazoMaximoMeses")]
    public int? PrazoMaximoMeses { get; set; }

    [JsonPropertyName("taxaJurosAnual")]
    public decimal? TaxaJurosAnual { get; set; }
}

