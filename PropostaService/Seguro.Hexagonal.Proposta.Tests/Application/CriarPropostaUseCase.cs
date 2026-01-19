using Moq;
using Seguro.Hexagonal.Application.UseCases;
using Seguro.Hexagonal.Domain.Commands;
using Seguro.Hexagonal.Proposta.Domain.Interfaces.Repositories;

namespace Seguro.Hexagonal.Proposta.Application.Tests;

public class CriarPropostaUseCaseTests
{
    [Fact]
    public async Task Deve_criar_proposta()
    {
        var repository = new Mock<IPropostaRepository>();

        var useCase = new CriarPropostaUseCase(repository.Object);

        var command = new CriarPropostaCommand("Cliente A");

        var id = await useCase.ExecuteAsync(command);

        Assert.NotEqual(Guid.Empty, id);
        repository.Verify(x => x.AddAsync(It.IsAny<Domain.Entities.Proposta>()), Times.Once);
    }
}
