using Microsoft.EntityFrameworkCore;

namespace ApiSimulador.Infrastructure.Data.Contexts;
public class ProdutoDbContext(DbContextOptions<ProdutoDbContext> options): DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
