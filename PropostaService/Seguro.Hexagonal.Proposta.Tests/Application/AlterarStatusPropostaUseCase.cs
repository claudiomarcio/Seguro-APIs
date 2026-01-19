using Moq;
using Seguro.Hexagonal.Application.Commands;
using Seguro.Hexagonal.Application.UseCases;
using Seguro.Hexagonal.Domain.Enums;
using Seguro.Hexagonal.Proposta.Domain.Interfaces.Repositories;

namespace Seguro.Hexagonal.Proposta.Application.Tests;

public class AlterarStatusPropostaUseCaseTests
{
    [Fact]
    public async Task Deve_aprovar_proposta()
    {
        var proposta = new Domain.Entities.Proposta("Cliente A");

        var repo = new Mock<IPropostaRepository>();
        repo.Setup(x => x.GetByIdAsync(proposta.Id)).ReturnsAsync(proposta);

        var useCase = new AlterarStatusPropostaUseCase(repo.Object);

        var command = new AlterarStatusPropostaCommand(
            proposta.Id,
            StatusProposta.Aprovada);

        await useCase.ExecuteAsync(command);

        Assert.Equal(StatusProposta.Aprovada, proposta.Status);
    }
}
