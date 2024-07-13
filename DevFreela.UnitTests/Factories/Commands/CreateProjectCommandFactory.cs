using AutoBogus;
using DevFreela.Application.Commands.CreateProject;

namespace DevFreela.UnitTests.Factories.Commands;

public class CreateProjectCommandFactory : AutoFaker<CreateProjectCommand>
{
    
    private CreateProjectCommandFactory()
    {
        Locale = "pt_BR";
        RuleFor(g => g.Title, f => f.Lorem.Word());
        RuleFor(g => g.Description, f => f.Lorem.Word());
        RuleFor(g => g.ClientId, f => f.Random.Int(1, int.MaxValue));
        RuleFor(g => g.FreelancerId, f => f.Random.Int(1, int.MaxValue));
        RuleFor(g => g.TotalCost, f => f.Random.Decimal());
    }

    public static CreateProjectCommandFactory Instance() => new();

}
