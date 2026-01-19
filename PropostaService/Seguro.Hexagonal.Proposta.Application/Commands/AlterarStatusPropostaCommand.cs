using Seguro.Hexagonal.Domain.Enums;

namespace Seguro.Hexagonal.Application.Commands;
public class AlterarStatusPropostaCommand
{
    public Guid PropostaId { get; }
    public StatusProposta NovoStatus { get; }

    public AlterarStatusPropostaCommand(Guid propostaId, StatusProposta novoStatus)
    {
        PropostaId = propostaId;
        NovoStatus = novoStatus;
    }
}