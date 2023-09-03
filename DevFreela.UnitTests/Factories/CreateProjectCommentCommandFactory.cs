using AutoBogus;
using DevFreela.Application.Commands.CreateProjectComment;

namespace DevFreela.UnitTests.Factories
{
    public class CreateProjectCommentCommandFactory : AutoFaker<CreateProjectCommentCommand>
    {
        
        public CreateProjectCommentCommandFactory()
        {
            Locale = "pt_BR";
            RuleFor(g => g.Comment, f => f.Lorem.Word());
            RuleFor(g => g.UserId, f => f.Random.Int(1, int.MaxValue));
            RuleFor(g => g.ProjectId, f => f.Random.Int(1, int.MaxValue));
        }

    }
}
