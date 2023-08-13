using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.DeleteProject
{

    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, Unit>
    {

        private readonly DevFreelaDbContext _dbContext;

        public DeleteProjectCommandHandler(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteProjectCommand command, CancellationToken cancellationToken)
        {
            var project = new Project();//GetById(command.id);
            project.Cancel();
            await _dbContext.SaveChangesAsync();
            return Unit.Value;
        }
    }
}