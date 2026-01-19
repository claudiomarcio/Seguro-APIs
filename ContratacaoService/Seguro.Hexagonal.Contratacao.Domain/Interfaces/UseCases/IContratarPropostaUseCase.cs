using Seguro.Hexagonal.Contratacao.Domain.Commands;
using Seguro.Hexagonal.Contratacao.Domain.ValueObjects;

namespace Seguro.Hexagonal.Contratacao.Domain.Interfaces.UseCases;

public interface IContratarPropostaUseCase
{
    Task<ContratarPropostaResult> ExecuteAsync(ContratarPropostaCommand command);
}

