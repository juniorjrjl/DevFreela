using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.DeleteProject
{

    public class FinishProjectCommandHandler : IRequestHandler<FinishProjectCommand, Unit>
    {

        private readonly DevFreelaDbContext _dbContext;

        public FinishProjectCommandHandler(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(FinishProjectCommand command, CancellationToken cancellationToken)
        {
            var project = new Project();//GetById(command.Id);
            project.Finish();
            await _dbContext.SaveChangesAsync();
            return Unit.Value;
        }
    }
}