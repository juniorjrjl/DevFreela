using AutoBogus;
using DevFreela.API.InputModel;

namespace DevFreela.UnitTests.Factories.InputModel;

public class LoginInputModelFactory : AutoFaker<LoginInputModel>
{
    private LoginInputModelFactory()
    {
        Locale = "pt_BR";
        RuleFor(g => g.Login, f => f.Lorem.Word());
        RuleFor(g => g.Password, f => f.Lorem.Word());
    }

    public static LoginInputModelFactory Instance() => new();
}