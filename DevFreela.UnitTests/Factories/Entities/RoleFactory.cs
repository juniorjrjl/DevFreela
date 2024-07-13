using Bogus;
using DevFreela.Core.Entities;
using DevFreela.Core.Enums;

namespace DevFreela.UnitTests.Factories.Entities;


public class RoleFactory: Faker<Role>
{

    private RoleFactory()
    {
        Locale = "pt_BR";
        CustomInstantiator(p => new(p.PickRandom<RoleNameEnum>()));
    }

    public static RoleFactory Instance() => new();

}
