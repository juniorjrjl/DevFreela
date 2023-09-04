using Bogus;
using DevFreela.Core.Entities;
using DevFreela.Core.Enums;

namespace DevFreela.UnitTests.Factories.Entities
{
    
    public class ProjectFactory: Faker<Project>
    {

        private ProjectFactory()
        {
            Locale = "pt_BR";
            RuleFor(p => p.Id, f=> f.Random.Number(1, int.MaxValue));
            RuleFor(p => p.Title, f => f.Lorem.Word());
            RuleFor(p => p.Description, f => f.Lorem.Word());
            RuleFor(p => p.TotalCost, f => f.Finance.Amount(1000, 999999, 2));
            RuleFor(p => p.Status, f => f.PickRandom<ProjectStatusEnum>());
            RuleFor(p => p.CreatedAt, f => f.Date.Recent());
            RuleFor(p => p.StartedAt, f => f.Date.Recent());
            RuleFor(p => p.FinishedAt, f => f.Date.Recent());
        }

        public static ProjectFactory Instance() => new();

    }

}
