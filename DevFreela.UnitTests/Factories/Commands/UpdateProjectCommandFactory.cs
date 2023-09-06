using AutoBogus;
using DevFreela.Application.Commands.UpdateProject;

namespace DevFreela.UnitTests.Factories.Commands
{
    public class UpdateProjectCommandFactort : AutoFaker<UpdateProjectCommand>
    {

        private UpdateProjectCommandFactort()
        {
            RuleFor(p => p.Title, f => f.Random.Word());
            RuleFor(p => p.Description, f => f.Random.Word());
            RuleFor(p => p.TotalCost, f => f.Random.Decimal());
            RuleFor(p => p.Id, f => f.Random.Int(1, int.MaxValue));
        }

        public static UpdateProjectCommandFactort Instance() => new();

    }
}
