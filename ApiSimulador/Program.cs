using ApiSimulador.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);
builder.AddApplicationConfig();
var app = builder.Build();
app.UseApplicationConfig();

app.Run();
