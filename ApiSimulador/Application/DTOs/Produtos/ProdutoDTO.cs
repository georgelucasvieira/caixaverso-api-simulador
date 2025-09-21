using System.Text.Json.Serialization;

namespace ApiSimulador.Application.DTOs.Produtos
{
    public class ProdutoDTO
    {
        public long Id { get; set; }
        public decimal ValorSolicitado  { get; set; }
        public int PrazoMeses { get; set; }
    }
}
