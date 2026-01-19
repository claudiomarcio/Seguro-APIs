using Microsoft.EntityFrameworkCore;
using Seguro.Hexagonal.Contratacao.Data.Context;
using Seguro.Hexagonal.Contratacao.Data.Repositories;
using ContratacaoEntities = Seguro.Hexagonal.Contratacao.Domain.Entities.Contratacao;

namespace Seguro.Hexagonal.Contratacao.Data.Tests;

public class ContratacaoRepositoryEfTests
{
    private static AppDbContext CriarContexto()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task Deve_salvar_contratacao_no_banco()
    {
        // Arrange
        using var context = CriarContexto();
        var repository = new ContratacaoRepositoryEf(context);

        var contratacao = new ContratacaoEntities(Guid.NewGuid());

        // Act
        await repository.AddAsync(contratacao);

        // Assert
        Assert.Equal(1, await context.Contratacoes.CountAsync());
    }

    [Fact]
    public async Task Deve_retornar_true_quando_contratacao_existir_para_proposta()
    {
        // Arrange
        using var context = CriarContexto();
        var repository = new ContratacaoRepositoryEf(context);

        var propostaId = Guid.NewGuid();
        await repository.AddAsync(new ContratacaoEntities(propostaId));

        // Act
        var existe = await repository.ExisteContratacaoParaPropostaAsync(propostaId);

        // Assert
        Assert.True(existe);
    }

    [Fact]
    public async Task Deve_retornar_false_quando_contratacao_nao_existir()
    {
        // Arrange
        using var context = CriarContexto();
        var repository = new ContratacaoRepositoryEf(context);

        // Act
        var existe = await repository
            .ExisteContratacaoParaPropostaAsync(Guid.NewGuid());

        // Assert
        Assert.False(existe);
    }
}
