

using Seguro.Hexagonal.Contratacao.Domain.Exceptions;

namespace Seguro.Hexagonal.Contratacao.Domain.Tests;

public class ContratacaoTests
{
    [Fact]
    public void Deve_criar_contratacao_com_proposta_valida()
    {
        var propostaId = Guid.NewGuid();

        var contratacao = new Entities.Contratacao(propostaId);

        Assert.NotEqual(Guid.Empty, contratacao.Id);
        Assert.Equal(propostaId, contratacao.PropostaId);
        Assert.True(contratacao.DataContratacao <= DateTime.UtcNow);
    }

    [Fact]
    public void Nao_deve_criar_contratacao_com_proposta_invalida()
    {
        Assert.Throws<DomainException>(() =>
            new Entities.Contratacao(Guid.Empty));
    }
}
