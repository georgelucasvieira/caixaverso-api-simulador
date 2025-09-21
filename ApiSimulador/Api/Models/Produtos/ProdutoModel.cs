using System.Text.Json.Serialization;

namespace ApiSimulador.Api.Models.Produtos
{
    public class ProdutoDTO
    {
        [JsonPropertyName("idProduto")]
        public long Id { get; set; }

        [JsonPropertyName("valorSolicitado")]
        public decimal ValorSolicitado { get; set; }
        
        [JsonPropertyName("prazoMeses")]
        public int PrazoMeses { get; set; }
    }
}
