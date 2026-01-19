using Microsoft.EntityFrameworkCore;
using Seguro.Hexagonal.Contratacao.Data.Context;
using Seguro.Hexagonal.Contratacao.Domain.Interfaces.Repositories;

namespace Seguro.Hexagonal.Contratacao.Data.Repositories;

public class ContratacaoRepositoryEf : IContratacaoRepository
{
    private readonly AppDbContext _context;

    public ContratacaoRepositoryEf(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExisteContratacaoParaPropostaAsync(Guid propostaId)
    {
        return await _context.Contratacoes
            .AsNoTracking()
            .AnyAsync(c => c.PropostaId == propostaId);
    }

    public async Task AddAsync(Domain.Entities.Contratacao contratacao)
    {
        await _context.Contratacoes.AddAsync(contratacao);
        await _context.SaveChangesAsync();
    }
}
