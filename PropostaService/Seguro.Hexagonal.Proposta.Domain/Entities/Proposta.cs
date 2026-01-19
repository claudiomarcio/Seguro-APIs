using Seguro.Hexagonal.Domain.Enums;
using Seguro.Hexagonal.Domain.Exceptions;

namespace Seguro.Hexagonal.Proposta.Domain.Entities;

public class Proposta
{
    public Guid Id { get; private set; }
    public string Cliente { get; private set; }
    public StatusProposta Status { get; private set; }
    public DateTime DataCriacao { get; private set; }

    public Proposta(string cliente)
    {
        if (string.IsNullOrWhiteSpace(cliente))
            throw new DomainException("Cliente é obrigatório.");

        Id = Guid.NewGuid();
        Cliente = cliente;
        Status = StatusProposta.EmAnalise;
        DataCriacao = DateTime.UtcNow;
    }

    public void Aprovar()
    {
        ValidarSePodeAlterarStatus();
        Status = StatusProposta.Aprovada;
    }

    public void Rejeitar()
    {
        ValidarSePodeAlterarStatus();
        Status = StatusProposta.Rejeitada;
    }

    private void ValidarSePodeAlterarStatus()
    {
        if (Status != StatusProposta.EmAnalise)
            throw new DomainException(
                $"Não é possível alterar o status quando a proposta está '{Status}'.");
    }
}
