using Microsoft.EntityFrameworkCore;
using Seguro.Hexagonal.Data.Context;
using PropostaEntities = Seguro.Hexagonal.Proposta.Domain.Entities.Proposta;
using Seguro.Hexagonal.Proposta.Domain.Interfaces.Repositories;

namespace Seguro.Hexagonal.Data.Repositories;

public class PropostaRepositoryEf : IPropostaRepository
{
    private readonly AppDbContext _context;

    public PropostaRepositoryEf(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(PropostaEntities proposta)
    {
        await _context.Propostas.AddAsync(proposta);
        await _context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<PropostaEntities>> GetAllAsync()
    {
        return await _context.Propostas
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<PropostaEntities?> GetByIdAsync(Guid id)
    {
        return await _context.Propostas
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task UpdateAsync(PropostaEntities proposta)
    {
        _context.Propostas.Update(proposta);
        await _context.SaveChangesAsync();
    }
}
