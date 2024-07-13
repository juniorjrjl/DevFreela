using Bogus;
using DevFreela.Core.Entities;

namespace DevFreela.UnitTests.Factories.Entities;


public class UserRoleFactory: Faker<UserRole>
{

    private UserRoleFactory()
    {
        Locale = "pt_BR";
        CustomInstantiator
        (p =>
            new
            (
                p.Random.Number(1, int.MaxValue)
            )
        );
    }

    public static UserRoleFactory Instance() => new();

}
