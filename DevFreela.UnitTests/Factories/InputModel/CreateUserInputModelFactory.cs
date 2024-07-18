using AutoBogus;
using DevFreela.API.InputModel;
using DevFreela.Core.Enums;

namespace DevFreela.UnitTests.Factories.InputModel;

public class NewUserInputModelFactory : AutoFaker<NewUserInputModel>
{
    
    private NewUserInputModelFactory()
    {
        Locale = "pt_BR";

        RuleFor(g => g.Name, f => f.Lorem.Word());
        RuleFor(g => g.Email, f => f.Lorem.Word());
        RuleFor(g => g.BirthDate, f => f.Date.Past(18));
        RuleFor(g => g.Password, f => f.Random.Word());
        RuleFor(g => g.PasswordConfirm, (f, u) => u.Password);
        RuleFor(g => g.Roles, f => f.Make(3, () => f.PickRandom<RoleNameEnum>()));
        RuleFor(g => g.SkillsId, f => f.Make(3, () => f.Random.Int()));
    }

    public static NewUserInputModelFactory Instance() => new();

}
