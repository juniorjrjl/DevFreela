using AutoBogus;
using DevFreela.Application.Commands.DeleteProject;

namespace DevFreela.UnitTests.Factories.Commands;

public class DeleteProjectCommandFactort : AutoFaker<DeleteProjectCommand>
{

    private DeleteProjectCommandFactort()
    {
        RuleFor(p => p.Id, f => f.Random.Int(1, Int32.MaxValue));
    }

    public static DeleteProjectCommandFactort Instance() => new();

}