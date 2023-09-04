using Bogus;
using DevFreela.Core.Entities;
using DevFreela.Core.Enums;

namespace DevFreela.UnitTests.Factories.Entities
{
    
    public class SkillFactory: Faker<Skill>
    {

        private SkillFactory()
        {
            Locale = "pt_BR";
            RuleFor(p => p.Id, f=> f.Random.Number(1, int.MaxValue));
            RuleFor(p => p.Description, f => f.Lorem.Word());
            RuleFor(p => p.CreatedAt, f => f.Date.Recent());
        }

        public static SkillFactory Instance() => new();

    }

}
