using Seguro.Hexagonal.Domain.Commands;
namespace Seguro.Hexagonal.Proposta.Domain.Interfaces.Usecases;
public interface ICriarPropostaUseCase
{
    Task<Guid> ExecuteAsync(CriarPropostaCommand command);
}
