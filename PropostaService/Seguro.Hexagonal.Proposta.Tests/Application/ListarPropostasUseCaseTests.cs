using Moq;
using Seguro.Hexagonal.Application.UseCases;
using Seguro.Hexagonal.Proposta.Domain.Interfaces.Repositories;

namespace Seguro.Hexagonal.Proposta.Application.Tests;

public class ListarPropostasUseCaseTests
{
    [Fact]
    public async Task Deve_retornar_lista_de_propostas_do_repositorio()
    {
        // Arrange
        var propostas = new List<Domain.Entities.Proposta>
        {
            new Domain.Entities.Proposta("Cliente A"),
            new Domain.Entities.Proposta("Cliente B")
        };

        var repositoryMock = new Mock<IPropostaRepository>();
        repositoryMock
            .Setup(r => r.GetAllAsync())
            .ReturnsAsync(propostas);

        var useCase = new ListarPropostasUseCase(repositoryMock.Object);

        // Act
        var result = await useCase.ExecuteAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal("Cliente A", result[0].Cliente);
        Assert.Equal("Cliente B", result[1].Cliente);

        repositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task Deve_retornar_lista_vazia_quando_nao_existirem_propostas()
    {
        // Arrange
        var repositoryMock = new Mock<IPropostaRepository>();
        repositoryMock
            .Setup(r => r.GetAllAsync())
            .ReturnsAsync(new List<Domain.Entities.Proposta>());

        var useCase = new ListarPropostasUseCase(repositoryMock.Object);

        // Act
        var result = await useCase.ExecuteAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);

        repositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
    }
}
