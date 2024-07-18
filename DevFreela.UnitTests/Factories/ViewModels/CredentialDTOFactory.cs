using AutoBogus;
using DevFreela.API.ViewModel;

namespace DevFreela.UnitTests.Factories.ViewModels;

public class CredentialViewModelFactory : AutoFaker<CredentialViewModel>
{
    private CredentialViewModelFactory()
    {
        Locale = "pt_BR";
        RuleFor(g => g.Token, f => f.Lorem.Word());
        RuleFor(g => g.ExpiresIn, f => f.Random.Int(999999));
    }

    public static CredentialViewModelFactory Instance() => new();
}