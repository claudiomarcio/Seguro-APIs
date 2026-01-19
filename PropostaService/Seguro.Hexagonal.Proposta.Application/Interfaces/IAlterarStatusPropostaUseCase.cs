using Seguro.Hexagonal.Application.Commands;

namespace Seguro.Hexagonal.Proposta.Application.Interfaces;
public interface IAlterarStatusPropostaUseCase
{
    Task ExecuteAsync(AlterarStatusPropostaCommand command);
}

