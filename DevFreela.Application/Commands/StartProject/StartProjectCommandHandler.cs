using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.DeleteProject
{

    public class StartProjectCommandHandler : IRequestHandler<StartProjectCommand, Unit>
    {

        private readonly DevFreelaDbContext _dbContext;

        public StartProjectCommandHandler(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(StartProjectCommand command, CancellationToken cancellationToken)
        {
            var project = new Project();//GetById(command.Id);
            project.Start();
            await _dbContext.SaveChangesAsync();
            return Unit.Value;
        }
    }
}