using Bogus;
using DevFreela.Core.Entities;
using DevFreela.Core.Enums;

namespace DevFreela.UnitTests.Factories.Entities;


public class ProjectFactory: Faker<Project>
{

    private ProjectFactory()
    {
        Locale = "pt_BR";
        CustomInstantiator(p => 
            new 
            (
                p.Lorem.Word(), 
                p.Lorem.Word(), 
                p.Random.Int(1, int.MaxValue),
                p.Random.Int(1, int.MaxValue),
                p.Random.Decimal()
            )
        );
    }

    public static ProjectFactory Instance() => new();

}
