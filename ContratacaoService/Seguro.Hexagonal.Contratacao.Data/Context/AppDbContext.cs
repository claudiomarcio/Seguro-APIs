using Microsoft.EntityFrameworkCore;

namespace Seguro.Hexagonal.Contratacao.Data.Context;

public class AppDbContext : DbContext
{
    public DbSet<Domain.Entities.Contratacao> Contratacoes => Set<Domain.Entities.Contratacao>();

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
