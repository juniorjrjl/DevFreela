using Bogus;
using DevFreela.Core.Entities;

namespace DevFreela.UnitTests.Factories.Entities
{
    
    public class UserSkillFactory: Faker<UserSkill>
    {

        private UserSkillFactory()
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

        public static UserSkillFactory Instance() => new();

    }

}
