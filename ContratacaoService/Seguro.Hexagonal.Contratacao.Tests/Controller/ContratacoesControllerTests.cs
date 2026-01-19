using Microsoft.AspNetCore.Mvc;
using Moq;
using Seguro.Hexagonal.Contratacao.Api.Controllers;
using Seguro.Hexagonal.Contratacao.Api.Models;
using Seguro.Hexagonal.Contratacao.Domain.Commands;
using Seguro.Hexagonal.Contratacao.Domain.Interfaces.UseCases;
using Seguro.Hexagonal.Contratacao.Domain.ValueObjects;

namespace Seguro.Hexagonal.Contratacao.Api.Tests;

public class ContratacoesControllerTests
{
    private readonly Mock<IContratarPropostaUseCase> _useCaseMock;
    private readonly ContratacoesController _controller;

    public ContratacoesControllerTests()
    {
        _useCaseMock = new Mock<IContratarPropostaUseCase>();

        _controller = new ContratacoesController(
            _useCaseMock.Object
        );
    }

    [Fact]
    public async Task Deve_retornar_201_quando_contratacao_sucesso()
    {
        // Arrange
        var request = new CriarContratacaoRequest
        {
            PropostaId = Guid.NewGuid()
        };

        var propostaResult = new ContratarPropostaResult(
            Guid.NewGuid(),
            Guid.NewGuid(),
            DateTime.UtcNow
        );

        _useCaseMock
            .Setup(x => x.ExecuteAsync(It.IsAny<ContratarPropostaCommand>()))
            .ReturnsAsync(propostaResult);

        // Act
        var result = await _controller.CriarAsync(request);

        // Assert
        var createdResult = Assert.IsType<CreatedResult>(result);
        Assert.NotNull(createdResult.Value);
    }
}
