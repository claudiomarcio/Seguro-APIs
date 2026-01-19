using Seguro.Hexagonal.Application.Commands;

namespace Seguro.Hexagonal.Proposta.Application.Interfaces;
public interface ICriarPropostaUseCase
{
    Task<Guid> ExecuteAsync(CriarPropostaCommand command);
}
