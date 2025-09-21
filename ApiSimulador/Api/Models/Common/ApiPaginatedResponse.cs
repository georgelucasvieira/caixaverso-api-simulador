namespace ApiSimulador.Api.Models.Common;

public class ApiPaginatedResponse<T>
{
    public int Pagina { get; set; }
    public int QtdRegistros { get; set; }
    public int QtdRegistrosPagina { get; set; }
    public List<T> Registros { get; set; } = [];

    public ApiPaginatedResponse<U> Select<U>(Func<T, U> selector)
    {
        return new ApiPaginatedResponse<U>
        {
            Pagina = Pagina,
            QtdRegistros = QtdRegistros,
            QtdRegistrosPagina = QtdRegistrosPagina,
            Registros = [.. Registros.Select(selector)]
        };
    }
}
