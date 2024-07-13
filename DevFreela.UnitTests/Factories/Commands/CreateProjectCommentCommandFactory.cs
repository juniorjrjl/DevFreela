using AutoBogus;
using DevFreela.Application.Commands.CreateProjectComment;

namespace DevFreela.UnitTests.Factories.Commands;

public class CreateProjectCommentCommandFactory : AutoFaker<CreateProjectCommentCommand>
{
    
    private CreateProjectCommentCommandFactory()
    {
        Locale = "pt_BR";
        RuleFor(g => g.Comment, f => f.Lorem.Word());
        RuleFor(g => g.UserId, f => f.Random.Int(1, int.MaxValue));
        RuleFor(g => g.ProjectId, f => f.Random.Int(1, int.MaxValue));
    }

    public static CreateProjectCommentCommandFactory Instance() => new();

}
