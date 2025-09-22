using ApiSimulador.Application.DTOs.Produtos;
using ApiSimulador.Application.DTOs.Simulacoes;

namespace ApiSimulador.Application.Services.Simulacoes;

public class SimulacaoService
{
    public SimulacaoDTO SimularTabelaPrice(ProdutoDTO produto, decimal valorSolicitado, int prazoMeses)
    {
        var taxaJurosEfetivaMensal = ConverterTaxaAnualParaMensal((decimal)produto.TaxaJurosAnual!);
        var prestacao = CalcularPrestacao(valorSolicitado, prazoMeses, taxaJurosEfetivaMensal);

        var simulacao = new SimulacaoDTO
        {
            Produto = produto,
            ValorSolicitado = valorSolicitado,
            PrazoMeses = prazoMeses,
            TaxaJurosEfetivaMensal = taxaJurosEfetivaMensal,
            ParcelaMensal = prestacao,
        };

        MontarMemoriaCalculo(simulacao, valorSolicitado);

        return simulacao;
    }

    private decimal CalcularPrestacao(decimal valorSolicitado, int prazoMeses, decimal taxaJurosMensal) {
        var numerador = valorSolicitado * (decimal)Math.Pow((double)taxaJurosMensal + 1, prazoMeses) * taxaJurosMensal;
        var denominador = (decimal)Math.Pow((double)taxaJurosMensal + 1, prazoMeses) - 1;
        return Math.Round(numerador / denominador, 2);
    }

    private decimal ConverterTaxaAnualParaMensal(decimal taxaAnual)
    {
        var taxaMensal = (decimal)Math.Pow((double)((taxaAnual / 100) + 1), (double)1 / 12) - 1;
        return taxaMensal;
    }

    private void MontarMemoriaCalculo(SimulacaoDTO simulacao, decimal valorSolicitado)
    {
        simulacao.MemoriaCalculos = new List<MemoriaCalculoDTO>();
        var saldoDevedor = valorSolicitado;
        for (int mes = 1; mes <= simulacao.PrazoMeses; mes++)
        {
            var juros = Math.Round(saldoDevedor * simulacao.TaxaJurosEfetivaMensal, 2);
            var amortizacao = Math.Round(simulacao.ParcelaMensal - juros, 2);
            var saldoDevedorInicial = saldoDevedor;

            if (simulacao.PrazoMeses == mes) 
            {
                amortizacao = saldoDevedor;
                juros = simulacao.ParcelaMensal - amortizacao;
                saldoDevedor = 0;
            }
            else
            {
                saldoDevedor -= amortizacao;
            }

            var calculo = new MemoriaCalculoDTO
            {
                Mes = mes,
                SaldoDevedorInicial = saldoDevedorInicial,
                SaldoDevedorFinal = saldoDevedor,
                Juros = juros,
                Amortizacao = amortizacao
            };

            simulacao.MemoriaCalculos.Add(calculo);
        }
    }

}
