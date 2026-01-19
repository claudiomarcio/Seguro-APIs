namespace Seguro.Hexagonal.Proposta.Domain.Interfaces.Repositories;
using PropostaEntities = Entities.Proposta;
public interface IPropostaRepository
{
    Task AddAsync(PropostaEntities proposta);
    Task<PropostaEntities?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<PropostaEntities>> GetAllAsync();
    Task UpdateAsync(PropostaEntities proposta);
}

