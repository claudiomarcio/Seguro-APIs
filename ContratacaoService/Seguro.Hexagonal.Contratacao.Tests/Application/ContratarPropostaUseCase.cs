using Microsoft.Extensions.Logging;
using Moq;
using Seguro.Hexagonal.Contratacao.Application.UseCases;
using Seguro.Hexagonal.Contratacao.Domain.Commands;
using Seguro.Hexagonal.Contratacao.Domain.Enums;
using Seguro.Hexagonal.Contratacao.Domain.Exceptions;
using Seguro.Hexagonal.Contratacao.Domain.Interfaces.Repositories;
using Seguro.Hexagonal.Contratacao.Domain.Interfaces.Services;
using Seguro.Hexagonal.Contratacao.Domain.ValueObjects;

namespace Seguro.Hexagonal.Contratacao.Application.Tests;

public class ContratarPropostaUseCaseTests
{
    private readonly Mock<IPropostaServiceClient> _propostaClientMock = new();
    private readonly Mock<IContratacaoRepository> _repositoryMock = new();
    private readonly Mock<ILogger<ContratarPropostaUseCase>> _loggerMock = new();

    [Fact]
    public async Task Deve_contratar_proposta_quando_aprovada()
    {
        // Arrange
        var propostaId = Guid.NewGuid();
        var propostaStatusResult = new PropostaStatusResult
        (
            propostaId,
            PropostaSituacao.Aprovada
        );

        _propostaClientMock
            .Setup(x => x.ObterStatusAsync(propostaId))
            .ReturnsAsync(propostaStatusResult);

        _repositoryMock
            .Setup(x => x.ExisteContratacaoParaPropostaAsync(propostaId))
            .ReturnsAsync(false);

        var useCase = new ContratarPropostaUseCase(
            _propostaClientMock.Object,
            _repositoryMock.Object,
            _loggerMock.Object);

        var command = new ContratarPropostaCommand(propostaId);

        // Act
        var result = await useCase.ExecuteAsync(command);

        // Assert
        Assert.NotNull(result);
        Assert.NotEqual(Guid.Empty, result.Id);
        Assert.Equal(propostaId, result.PropostaId);
        Assert.True(result.DataContratacao <= DateTime.UtcNow);

        _repositoryMock.Verify(
            x => x.AddAsync(It.IsAny<Domain.Entities.Contratacao>()),
            Times.Once);
    }


    [Fact]
    public async Task Nao_deve_contratar_proposta_nao_aprovada()
    {
        var propostaId = Guid.NewGuid();

        var propostaStatusResult = new PropostaStatusResult
       (
           propostaId,
           PropostaSituacao.NaoAprovada
       );

        _propostaClientMock
            .Setup(x => x.ObterStatusAsync(propostaId))
            .ReturnsAsync(propostaStatusResult);

        var useCase = new ContratarPropostaUseCase(
            _propostaClientMock.Object,
            _repositoryMock.Object,
            _loggerMock.Object);

        var command = new ContratarPropostaCommand(propostaId);

        await Assert.ThrowsAsync<DomainException>(() =>
            useCase.ExecuteAsync(command));
    }

    [Fact]
    public async Task Nao_deve_contratar_proposta_ja_contratada()
    {
        var propostaId = Guid.NewGuid();

        var propostaStatusResult = new PropostaStatusResult
        (
           propostaId,
           PropostaSituacao.Aprovada
        );

        _propostaClientMock
            .Setup(x => x.ObterStatusAsync(propostaId))
            .ReturnsAsync(propostaStatusResult);

        _repositoryMock
            .Setup(x => x.ExisteContratacaoParaPropostaAsync(propostaId))
            .ReturnsAsync(true);

        var useCase = new ContratarPropostaUseCase(
            _propostaClientMock.Object,
            _repositoryMock.Object,
            _loggerMock.Object);

        var command = new ContratarPropostaCommand(propostaId);

        await Assert.ThrowsAsync<DomainException>(() =>
            useCase.ExecuteAsync(command));
    }
}
