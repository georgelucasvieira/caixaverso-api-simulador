using ApiSimulador.Application.DTOs.Produtos;

namespace ApiSimulador.Application.DTOs.Simulacoes;

public class SimulacaoDTO
{
    public ProdutoDTO Produto {  get; set; }
    public decimal ValorSolicitado { get; set; }
    public int PrazoMeses { get; set; }
    public decimal TaxaJurosEfetivaMensal {  get; set; }
    public decimal ParcelaMensal { get; set; }
    public List<MemoriaCalculoDTO> MemoriaCalculos { get; set; }
}
