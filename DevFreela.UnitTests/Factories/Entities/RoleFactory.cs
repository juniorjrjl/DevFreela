using Bogus;
using DevFreela.Core.Entities;
using DevFreela.Core.Enums;

namespace DevFreela.UnitTests.Factories.Entities
{
    
    public class RoleFactory: Faker<Role>
    {

        private RoleFactory()
        {
            Locale = "pt_BR";
            RuleFor(p => p.Id, f=> f.Random.Number(1, int.MaxValue));
            RuleFor(p => p.Name, f => f.PickRandom<RoleNameEnum>());
        }

        public static RoleFactory Instance() => new();

    }

}
