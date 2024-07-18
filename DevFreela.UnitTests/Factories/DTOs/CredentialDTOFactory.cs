using AutoBogus;
using DevFreela.Application.Commands;
using DevFreela.Core.DTOs;

namespace DevFreela.UnitTests.Factories.Commands;

public class CredentialDTOFactory : AutoFaker<CredentialDTO>
{
    private CredentialDTOFactory()
    {
        Locale = "pt_BR";
        RuleFor(g => g.Token, f => f.Lorem.Word());
        RuleFor(g => g.ExpiresIn, f => f.Random.Int(999999));
    }

    public static CredentialDTOFactory Instance() => new();
}