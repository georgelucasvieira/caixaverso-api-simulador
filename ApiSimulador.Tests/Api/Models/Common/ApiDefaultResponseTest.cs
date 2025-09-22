using ApiSimulador.Api.Models.Common;
using System.Text.Json;
using Xunit;

public class ApiDefaultResponseTest
{
    [Fact]
    public void ApiDefaultResponse_ShouldAssignValuesCorrectly()
    {
        var response = new ApiDefaultResponse(true, "Operação realizada com sucesso");

        Assert.True(response.Sucesso);
        Assert.Equal("Operação realizada com sucesso", response.Mensagem);
    }

    [Fact]
    public void ApiDefaultResponse_ShouldSerializeToJsonWithCustomNames()
    {
        var response = new ApiDefaultResponse(false, "Erro ao processar");

        var json = JsonSerializer.Serialize(response);
        Assert.Contains("\"sucesso\":false", json);
        Assert.Contains("\"mensagem\":\"Erro ao processar\"", json);
    }

    [Fact]
    public void ApiDefaultResponse_ShouldDeserializeFromJsonCorrectly()
    {
        var json = "{\"sucesso\":true,\"mensagem\":\"Tudo certo\"}";
        var response = JsonSerializer.Deserialize<ApiDefaultResponse>(json);

        Assert.NotNull(response);
        Assert.True(response!.Sucesso);
        Assert.Equal("Tudo certo", response.Mensagem);
    }
}