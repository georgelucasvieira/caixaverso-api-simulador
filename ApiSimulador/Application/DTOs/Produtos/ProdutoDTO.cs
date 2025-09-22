using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiSimulador.Application.DTOs.Produtos;

public class ProdutoDTO
{
    public long CoProduto { get; set; }
    public string? NomeProduto { get; set; }
    public int? PrazoMaximoMeses { get; set; }
    public decimal? TaxaJurosAnual { get; set; }
}
