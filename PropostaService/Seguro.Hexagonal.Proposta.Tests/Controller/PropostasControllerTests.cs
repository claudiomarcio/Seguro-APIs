using Microsoft.AspNetCore.Mvc;
using Moq;
using Seguro.Hexagonal.Api.Controllers;
using Seguro.Hexagonal.Domain.Commands;
using Seguro.Hexagonal.Domain.Enums;
using Seguro.Hexagonal.Domain.Exceptions;
using Seguro.Hexagonal.Proposta.Api.Models;
using Seguro.Hexagonal.Proposta.Domain.Interfaces.Usecases;

namespace Seguro.Hexagonal.Proposta.Api.Tests;

public class PropostasControllerTests
{
    private readonly Mock<ICriarPropostaUseCase> _criarMock;
    private readonly Mock<IListarPropostasUseCase> _listarMock;
    private readonly Mock<IAlterarStatusPropostaUseCase> _alterarStatusMock;

    private readonly PropostasController _controller;

    public PropostasControllerTests()
    {
        _criarMock = new Mock<ICriarPropostaUseCase>();
        _listarMock = new Mock<IListarPropostasUseCase>();
        _alterarStatusMock = new Mock<IAlterarStatusPropostaUseCase>();

        _controller = new PropostasController(
            _criarMock.Object,
            _listarMock.Object,
            _alterarStatusMock.Object
        );
    }

    [Fact]
    public async Task Criar_Deve_retornar_201_quando_sucesso()
    {
        // Arrange
        var request = new CriarPropostaRequest { Cliente = "Cliente Teste" };

        _criarMock
            .Setup(x => x.ExecuteAsync(It.IsAny<CriarPropostaCommand>()))
            .ReturnsAsync(Guid.NewGuid());

        // Act
        var result = await _controller.Criar(request);

        // Assert
        Assert.IsType<CreatedAtActionResult>(result);
    }

    [Fact]
    public async Task Criar_Deve_lancar_DomainException()
    {
        // Arrange
        _criarMock
            .Setup(x => x.ExecuteAsync(It.IsAny<CriarPropostaCommand>()))
            .ThrowsAsync(new DomainException("Erro de domínio"));

        // Act & Assert
        var exception = await Assert.ThrowsAsync<DomainException>(() =>
            _controller.Criar(new CriarPropostaRequest()));

        Assert.Equal("Erro de domínio", exception.Message);
    }


    [Fact]
    public async Task Listar_Deve_retornar_200_com_lista()
    {
        // Arrange
        var propostas = new List<Domain.Entities.Proposta>
        {
            new Domain.Entities.Proposta("Cliente A"),
            new Domain.Entities.Proposta("Cliente B")
        };

        _listarMock
            .Setup(x => x.ExecuteAsync())
            .ReturnsAsync(propostas);

        // Act
        var result = await _controller.Listar();

        // Assert
        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsAssignableFrom<IEnumerable<PropostaResponse>>(ok.Value);

        Assert.Equal(2, response.Count());
    }

    [Fact]
    public async Task ObterPorId_Deve_retornar_200_quando_encontrar()
    {
        // Arrange
        var proposta = new Domain.Entities.Proposta("Cliente A");

        _listarMock
            .Setup(x => x.ExecuteAsync())
            .ReturnsAsync(new List<Domain.Entities.Proposta> { proposta });

        // Act
        var result = await _controller.ObterPorId(proposta.Id);

        // Assert
        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<PropostaResponse>(ok.Value);

        Assert.Equal(proposta.Id, response.Id);
    }

    [Fact]
    public async Task ObterPorId_Deve_retornar_404_quando_nao_encontrar()
    {
        // Arrange
        _listarMock
            .Setup(x => x.ExecuteAsync())
            .ReturnsAsync(new List<Domain.Entities.Proposta>());

        // Act
        var result = await _controller.ObterPorId(Guid.NewGuid());

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task AlterarStatus_Deve_retornar_204_quando_sucesso()
    {
        // Arrange
        var request = new AlterarStatusPropostaRequest
        {
            Status = StatusProposta.Aprovada.ToString()
        };

        _alterarStatusMock
            .Setup(x => x.ExecuteAsync(It.IsAny<AlterarStatusPropostaCommand>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.AlterarStatus(Guid.NewGuid(), request);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task AlterarStatus_Deve_retornar_400_quando_status_invalido()
    {
        // Arrange
        var request = new AlterarStatusPropostaRequest
        {
            Status = "StatusInvalido"
        };

        // Act
        var result = await _controller.AlterarStatus(Guid.NewGuid(), request);

        // Assert
        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Status inválido.", badRequest.Value);
    }

    [Fact]
    public async Task AlterarStatus_Deve_retornar_400_quando_domain_exception()
    {
        // Arrange
        var request = new AlterarStatusPropostaRequest
        {
            Status = StatusProposta.Aprovada.ToString()
        };

        _alterarStatusMock
            .Setup(x => x.ExecuteAsync(It.IsAny<AlterarStatusPropostaCommand>()))
            .ThrowsAsync(new DomainException("Erro domínio"));

        // Act & Assert
        var exception = await Assert.ThrowsAsync<DomainException>(() =>
            _controller.AlterarStatus(Guid.NewGuid(), request));

        Assert.Equal("Erro domínio", exception.Message);
    }
}
