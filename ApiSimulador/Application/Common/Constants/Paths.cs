namespace ApiSimulador.Application.Common.Constants;

public static class Paths
{
    public const string Health = "/health";
    public const string Simulacao = "/simulacao";
    public const string Telemetria = "/telemetria";
    public const string SimulacaoV1 = $"/v1{Simulacao}";
    public const string TelemetriaV1 = $"/v1{Telemetria}";
    public const string SimulaEmprestimo = "/simula-emprestimo";
    public const string SimulacaoBuscaPaginada = "/busca-paginada";
    public const string SimulacaoBuscaPorDataEProduto = "/busca-por-data-e-produto";
}
