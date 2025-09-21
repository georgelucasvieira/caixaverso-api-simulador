using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiSimulador.Api.Models.Produtos
{
    public class CreateProdutoRequest
    {
        [Required]
        [JsonPropertyName("nomeProduto")]
        public required string NomeProduto {  get; set; }
       
        [Required]
        [JsonPropertyName("prazoMaximoMeses")]
        public int PrazoMaximoMeses { get; set; }

        [Required]
        [JsonPropertyName("taxaJurosAnual")]
        public decimal TaxaJurosAnual {  get; set; }
    }
}
