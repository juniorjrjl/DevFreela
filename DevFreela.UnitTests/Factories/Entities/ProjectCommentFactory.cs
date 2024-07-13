using Bogus;
using DevFreela.Core.Entities;

namespace DevFreela.UnitTests.Factories.Entities;


public class ProjectCommentFactory: Faker<ProjectComment>
{

    private ProjectCommentFactory()
    {
        Locale = "pt_BR";
        CustomInstantiator(p => 
            new 
            (
                p.Lorem.Word(),
                p.Random.Int(1, int.MaxValue),
                p.Random.Int(1, int.MaxValue)
            )
        );
    }

    public static ProjectCommentFactory Instance() => new();

}
