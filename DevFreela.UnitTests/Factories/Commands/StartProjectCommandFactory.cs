using AutoBogus;
using DevFreela.Application.Commands.StartProject;

namespace DevFreela.UnitTests.Factories.Commands
{
    public class StartProjectCommandFactort : AutoFaker<StartProjectCommand>
    {

        private StartProjectCommandFactort()
        {
            RuleFor(p => p.Id, f => f.Random.Int(1, Int32.MaxValue));
        }

        public static StartProjectCommandFactort Instance() => new();

    }
}
