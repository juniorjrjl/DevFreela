using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.UpdateProject
{

    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, Project>
    {

        private readonly DevFreelaDbContext _dbContext; 

        public UpdateProjectCommandHandler(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Project> Handle(UpdateProjectCommand command, CancellationToken cancellationToken)
        {
            var toUpdate = new Project
            {
                Title = command.Title,
                Description = command.Description,
                TotalCost = command.TotalCost
            };//GetById(command.Id);
            await _dbContext.SaveChangesAsync();
            return toUpdate;
        }
    }
}