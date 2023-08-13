using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.CreateProject
{

    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Project>
    {

        private readonly DevFreelaDbContext _dbContext; 

        public CreateProjectCommandHandler(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Project> Handle(CreateProjectCommand command, CancellationToken cancellationToken)
        {
            var entity = new Project
            {
                Title = command.Title,
                Description = command.Description,
                ClientId = command.ClientId,
                FreelancerId = command.FreelancerId,
                TotalCost = command.TotalCost
            };
            await _dbContext.Projects.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }

}
