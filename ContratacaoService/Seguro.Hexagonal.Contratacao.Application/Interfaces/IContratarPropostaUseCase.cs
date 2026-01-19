using Seguro.Hexagonal.Contratacao.Application.Commands;
using Seguro.Hexagonal.Contratacao.Domain.ValueObjects;

namespace Seguro.Hexagonal.Contratacao.Application.Interfaces;

public interface IContratarPropostaUseCase
{
    Task<ContratarPropostaResult> ExecuteAsync(ContratarPropostaCommand command);
}

