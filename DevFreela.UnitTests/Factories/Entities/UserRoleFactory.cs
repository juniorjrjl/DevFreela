using Bogus;
using DevFreela.Core.Entities;
using DevFreela.Core.Enums;

namespace DevFreela.UnitTests.Factories.Entities
{
    
    public class UserRoleFactory: Faker<UserRole>
    {

        private UserRoleFactory()
        {
            Locale = "pt_BR";
            RuleFor(p => p.RoleId, f => f.Random.Number(1, int.MaxValue));
            RuleFor(p => p.Role, f => RoleFactory.Instance().Generate());
            RuleFor(p => p.UserId, f => f.Random.Number(1, int.MaxValue));
            RuleFor(p => p.User, f => UserFactory.Instance().Generate());
        }

        public static UserRoleFactory Instance() => new();

    }

}
