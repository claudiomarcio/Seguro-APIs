using Microsoft.EntityFrameworkCore;
using PropostaEntities = Seguro.Hexagonal.Proposta.Domain.Entities.Proposta;

namespace Seguro.Hexagonal.Data.Context;

public class AppDbContext : DbContext
{
    public DbSet<PropostaEntities> Propostas => Set<PropostaEntities>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
