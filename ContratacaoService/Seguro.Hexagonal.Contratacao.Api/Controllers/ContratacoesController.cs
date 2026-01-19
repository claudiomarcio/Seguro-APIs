using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Seguro.Hexagonal.Contratacao.Api.Models;
using Seguro.Hexagonal.Contratacao.Application.Commands;
using Seguro.Hexagonal.Contratacao.Application.Interfaces;

namespace Seguro.Hexagonal.Contratacao.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/contratacoes")]
public class ContratacoesController : ControllerBase
{
    private readonly IContratarPropostaUseCase _useCase;

    public ContratacoesController(IContratarPropostaUseCase useCase)
    {
        _useCase = useCase;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CriarAsync(
        [FromBody] CriarContratacaoRequest request)
    {
        var command = new ContratarPropostaCommand(request.PropostaId);

        var result = await _useCase.ExecuteAsync(command);

        return Created(
            $"/api/contratacoes/{result.Id}",
            result);
    }
}
