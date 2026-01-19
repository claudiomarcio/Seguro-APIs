using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Seguro.Hexagonal.Domain.Commands;
using Seguro.Hexagonal.Domain.Enums;
using Seguro.Hexagonal.Proposta.Api.Models;
using Seguro.Hexagonal.Proposta.Domain.Interfaces.Usecases;

namespace Seguro.Hexagonal.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/propostas")]
public class PropostasController : ControllerBase
{
    private readonly ICriarPropostaUseCase _criarUseCase;
    private readonly IListarPropostasUseCase _listarUseCase;
    private readonly IAlterarStatusPropostaUseCase _alterarStatusUseCase;

    public PropostasController(
        ICriarPropostaUseCase criarUseCase,
        IListarPropostasUseCase listarUseCase,
        IAlterarStatusPropostaUseCase alterarStatusUseCase)
    {
        _criarUseCase = criarUseCase;
        _listarUseCase = listarUseCase;
        _alterarStatusUseCase = alterarStatusUseCase;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Criar([FromBody] CriarPropostaRequest request)
    {
        var command = new CriarPropostaCommand(request.Cliente);
        var id = await _criarUseCase.ExecuteAsync(command);

        return CreatedAtAction(nameof(ObterPorId), new { id }, null);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Listar()
    {
        var propostas = await _listarUseCase.ExecuteAsync();

        var response = propostas.Select(p => new PropostaResponse
        {
            Id = p.Id,
            Cliente = p.Cliente,
            Status = p.Status.ToString(),
            DataCriacao = p.DataCriacao
        });

        return Ok(response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        var propostas = await _listarUseCase.ExecuteAsync();
        var proposta = propostas.FirstOrDefault(p => p.Id == id);

        if (proposta == null)
            return NotFound();

        return Ok(new PropostaResponse
        {
            Id = proposta.Id,
            Cliente = proposta.Cliente,
            Status = proposta.Status.ToString(),
            DataCriacao = proposta.DataCriacao
        });
    }

    [HttpPatch("{id}/status")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AlterarStatus(
        Guid id,
        [FromBody] AlterarStatusPropostaRequest request)
    {
        if (!Enum.TryParse<StatusProposta>(request.Status, true, out var status))
            return BadRequest("Status inválido.");

        var command = new AlterarStatusPropostaCommand(id, status);
        await _alterarStatusUseCase.ExecuteAsync(command);

        return NoContent();
    }
}
