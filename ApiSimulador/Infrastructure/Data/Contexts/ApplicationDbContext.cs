using ApiSimulador.Domain.Entities.Produtos;
using Microsoft.EntityFrameworkCore;

namespace ApiSimulador.Infrastructure.Data.Contexts;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): DbContext(options)
{
    public DbSet<Produto> Produtos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
