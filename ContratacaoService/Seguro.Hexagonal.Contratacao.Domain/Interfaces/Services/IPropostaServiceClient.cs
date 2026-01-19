using Seguro.Hexagonal.Contratacao.Domain.ValueObjects;

namespace Seguro.Hexagonal.Contratacao.Domain.Interfaces.Services;


public interface IPropostaServiceClient
{
    Task<PropostaStatusResult> ObterStatusAsync(Guid propostaId);
}