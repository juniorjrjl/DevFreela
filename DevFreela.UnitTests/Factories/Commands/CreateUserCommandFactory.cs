using AutoBogus;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.CreateUser;
using DevFreela.Core.Enums;

namespace DevFreela.UnitTests.Factories.Commands
{
    public class CreateUserCommandFactory : AutoFaker<CreateUserCommand>
    {
        
        private CreateUserCommandFactory()
        {
            Locale = "pt_BR";
            RuleFor(g => g.Name, f => f.Lorem.Word());
            RuleFor(g => g.Email, f => f.Lorem.Word());
            RuleFor(g => g.BirthDate, f => f.Date.Past(18));
            RuleFor(g => g.Password, f => f.Random.Word());
            RuleFor(g => g.Roles, f => f.Make(3, () => f.PickRandom<RoleNameEnum>()));
            RuleFor(g => g.SkillsId, f => f.Make(3, () => f.Random.Int()));
        }

        public static CreateUserCommandFactory Instance() => new();

    }
}
