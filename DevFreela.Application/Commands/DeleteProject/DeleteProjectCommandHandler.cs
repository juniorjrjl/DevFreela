using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.DeleteProject
{

    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand>
    {

        private readonly IProjectRepository _projectRepository;
        private readonly IProjectQueryRepository _projectQueryRepository;

        public DeleteProjectCommandHandler(IProjectRepository projectRepository, IProjectQueryRepository projectQueryRepository)
        {
            _projectRepository = projectRepository;
            _projectQueryRepository = projectQueryRepository;
        }

        public async Task Handle(DeleteProjectCommand command, CancellationToken cancellationToken)
        {
            var toUpdate = await _projectQueryRepository.GetByIdAsync(command.Id);
            toUpdate.Cancel();
            await _projectRepository.UpdateAsync(toUpdate);
            return;
        }
    }
}