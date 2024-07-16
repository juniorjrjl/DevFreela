using AutoBogus;
using DevFreela.Application.Commands.DeleteProject;

namespace DevFreela.UnitTests.Factories.Commands;

public class DeleteProjectCommandFactort : AutoFaker<DeleteProjectCommand>
{

    private DeleteProjectCommandFactort()
    {
        RuleFor(p => p.Id, f => f.Random.Int(1, int.MaxValue));
    }

    public static DeleteProjectCommandFactort Instance() => new();

}