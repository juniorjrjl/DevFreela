using AutoBogus;
using DevFreela.Application.Commands.FinishProject;

namespace DevFreela.UnitTests.Factories.Commands;

public class FinishProjectCommandFactort : AutoFaker<FinishProjectCommand>
{

    private FinishProjectCommandFactort()
    {
        RuleFor(p => p.Id, f => f.Random.Int(1, Int32.MaxValue));
    }

    public static FinishProjectCommandFactort Instance() => new();

}
