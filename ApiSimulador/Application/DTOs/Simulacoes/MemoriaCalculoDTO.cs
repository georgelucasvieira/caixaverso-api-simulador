namespace ApiSimulador.Application.DTOs.Simulacoes;

public class MemoriaCalculoDTO
{
    public int Mes { get; set; }
    public decimal SaldoDevedorInicial { get; set; }
    public decimal Juros { get; set; }
    public decimal Amortizacao { get; set; }
    public decimal SaldoDevedorFinal { get; set; }
}
