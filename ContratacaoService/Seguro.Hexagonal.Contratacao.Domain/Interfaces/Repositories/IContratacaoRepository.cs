namespace Seguro.Hexagonal.Contratacao.Domain.Interfaces.Repositories;
using Contratacao = Entities.Contratacao;

public interface IContratacaoRepository
{
    Task<bool> ExisteContratacaoParaPropostaAsync(Guid propostaId);
    Task AddAsync(Contratacao contratacao);
}
