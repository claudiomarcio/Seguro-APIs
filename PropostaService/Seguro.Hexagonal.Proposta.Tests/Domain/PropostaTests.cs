using Seguro.Hexagonal.Domain.Enums;
using Seguro.Hexagonal.Domain.Exceptions;

namespace Seguro.Hexagonal.Proposta.Domain.Tests;

public class PropostaTests
{
    [Fact]
    public void Deve_criar_proposta_com_status_inicial_em_analise()
    {
        var proposta = new Entities.Proposta("Cliente A");

        Assert.Equal(StatusProposta.EmAnalise, proposta.Status);
    }

    [Fact]
    public void Deve_aprovar_proposta_em_analise()
    {
        var proposta = new Entities.Proposta("Cliente A");

        proposta.Aprovar();

        Assert.Equal(StatusProposta.Aprovada, proposta.Status);
    }

    [Fact]
    public void Nao_deve_aprovar_proposta_ja_aprovada()
    {
        var proposta = new Entities.Proposta("Cliente A");
        proposta.Aprovar();

        Assert.Throws<DomainException>(() => proposta.Aprovar());
    }

    [Fact]
    public void Deve_rejeitar_proposta_em_analise()
    {
        var proposta = new Entities.Proposta("Cliente A");

        proposta.Rejeitar();

        Assert.Equal(StatusProposta.Rejeitada, proposta.Status);
    }
}
