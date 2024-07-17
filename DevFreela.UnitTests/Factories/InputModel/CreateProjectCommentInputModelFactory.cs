using AutoBogus;
using DevFreela.API.InputModel;

namespace DevFreela.UnitTests.Factories.Commands;

public class CreateProjectCommentInputModelFactory : AutoFaker<CreateCommentInputModel>
{
    
    private CreateProjectCommentInputModelFactory()
    {
        Locale = "pt_BR";
        RuleFor(g => g.Comment, f => f.Lorem.Word());
        RuleFor(g => g.UserId, f => f.Random.Int(1, int.MaxValue));
    }

    public static CreateProjectCommentInputModelFactory Instance() => new();

}
