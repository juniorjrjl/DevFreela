using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.UpdateProject
{

    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, Project>
    {

        private readonly IProjectRepository _projectRepository;
        private readonly IProjectQueryRepository _projectQueryRepository;

        public UpdateProjectCommandHandler(IProjectRepository projectRepository, IProjectQueryRepository projectQueryRepository)
        {
            _projectRepository = projectRepository;
            _projectQueryRepository = projectQueryRepository;
        }

        public async Task<Project> Handle(UpdateProjectCommand command, CancellationToken cancellationToken)
        {
            var toUpdate = await _projectQueryRepository.GetByIdAsync(command.Id);
            toUpdate.Title = command.Title;
            toUpdate.Description = command.Description;
            toUpdate.TotalCost = command.TotalCost;
            return await _projectRepository.UpdateAsync(toUpdate);
        }
    }
}