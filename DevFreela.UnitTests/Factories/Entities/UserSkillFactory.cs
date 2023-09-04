using Bogus;
using DevFreela.Core.Entities;
using DevFreela.Core.Enums;

namespace DevFreela.UnitTests.Factories.Entities
{
    
    public class UserSkillFactory: Faker<UserSkill>
    {

        private UserSkillFactory()
        {
            Locale = "pt_BR";
            RuleFor(p => p.SkillId, f => f.Random.Number(1, int.MaxValue));
            RuleFor(p => p.Skill, f => SkillFactory.Instance().Generate());
            RuleFor(p => p.UserId, f => f.Random.Number(1, int.MaxValue));
            RuleFor(p => p.User, f => UserFactory.Instance().Generate());
        }

        public static UserSkillFactory Instance() => new();

    }

}
