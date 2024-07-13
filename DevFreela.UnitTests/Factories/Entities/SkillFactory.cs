using Bogus;
using DevFreela.Core.Entities;
using Microsoft.VisualBasic;

namespace DevFreela.UnitTests.Factories.Entities;


public class SkillFactory: Faker<Skill>
{

    private SkillFactory()
    {
        Locale = "pt_BR";
        CustomInstantiator
        ( p=> 
            new
            (
                p.Random.Number(1, int.MaxValue),
                p.Lorem.Word(),
                p.Date.Recent(),
                new List<UserSkill>()
            )

        );
    }

    public static SkillFactory Instance() => new();

}
