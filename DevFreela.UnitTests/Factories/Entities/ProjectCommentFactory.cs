using Bogus;
using DevFreela.Core.Entities;

namespace DevFreela.UnitTests.Factories.Entities
{
    
    public class ProjectCommentFactory: Faker<ProjectComment>
    {

        private ProjectCommentFactory()
        {
            Locale = "pt_BR";
            RuleFor(p => p.Id, f=> f.Random.Number(1, int.MaxValue));
            RuleFor(p => p.Comment, f => f.Lorem.Word());
            RuleFor(g => g.ProjectId, f => f.Random.Int(1, int.MaxValue));
            RuleFor(g => g.Project, f => ProjectFactory.Instance().Generate());
            RuleFor(g => g.UserId, f => f.Random.Int(1, int.MaxValue));
            RuleFor(g => g.User, f => UserFactory.Instance().Generate());
            RuleFor(p => p.CreatedAt, f => f.Date.Recent());
        }

        public static ProjectCommentFactory Instance() => new();

    }

}
