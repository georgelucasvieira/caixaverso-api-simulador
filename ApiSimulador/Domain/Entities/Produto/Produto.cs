using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiSimulador.Domain.Entities.Produto;

[Table("PRODUTO")]
public class Produto
{
    [Key]
    [Column("CO_PRODUTO")]
    public int CoProduto { get; set; }

    [Required]
    [Column("NO_PRODUTO")]
    [MaxLength(200)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [Column("PC_TAXA_JUROS_ANUAL")]
    public decimal TaxaJurosAnual { get; set; }

    [Required]
    [Column("PRAZO_MAXIMO_MESES")]
    public int PrazoMaximoMeses { get; set; }
}

