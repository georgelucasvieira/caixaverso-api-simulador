namespace ApiSimulador.Api.Models.Common;

public class ApiPaginatedResponse<T>
{
    public int Pagina { get; set; }
    public long QtdRegistros { get; set; }
    public int QtdRegistrosPagina { get; set; }
    public List<T> Registros { get; set; } = [];
}
