using ApiSimulador.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ApiSimulador.Api.Configurations;

public static class DatabaseConfig
{
    public static void AddDatabaseConfig(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite("Data Source=app.db"));   
    }
}
