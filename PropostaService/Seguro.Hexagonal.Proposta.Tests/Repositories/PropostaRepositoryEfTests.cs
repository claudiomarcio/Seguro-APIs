using Microsoft.EntityFrameworkCore;
using Seguro.Hexagonal.Data.Context;
using Seguro.Hexagonal.Data.Repositories;

namespace Seguro.Hexagonal.Proposta.Data.Tests;

public class PropostaRepositoryEfTests
{
    private static AppDbContext CriarContexto()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task Deve_salvar_proposta()
    {
        using var context = CriarContexto();
        var repo = new PropostaRepositoryEf(context);

        await repo.AddAsync(new Domain.Entities.Proposta("Cliente A"));

        Assert.Equal(1, await context.Propostas.CountAsync());
    }
}
