using AutoBogus;
using DevFreela.Application.Commands;

namespace DevFreela.UnitTests.Factories.Commands;

public class UserLoginCommandFactory : AutoFaker<UserLoginCommand>
{
    private UserLoginCommandFactory()
    {
        Locale = "pt_BR";
        RuleFor(g => g.Login, f => f.Lorem.Word());
        RuleFor(g => g.Password, f => f.Lorem.Word());
    }

    public static UserLoginCommandFactory Instance() => new();
}