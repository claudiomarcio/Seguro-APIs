using Seguro.Hexagonal.Domain.Enums;

namespace Seguro.Hexagonal.Domain.Commands;
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