using Seguro.Hexagonal.Domain.Commands;

namespace Seguro.Hexagonal.Proposta.Domain.Interfaces.Usecases;
public interface IAlterarStatusPropostaUseCase
{
    Task ExecuteAsync(AlterarStatusPropostaCommand command);
}

