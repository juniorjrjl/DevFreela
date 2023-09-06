using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.FinishProject
{

    public class FinishProjectCommandHandler : IRequestHandler<FinishProjectCommand>
    {

        private readonly IProjectRepository _projectRepository;
        private readonly IProjectQueryRepository _projectQueryRepository;

        public FinishProjectCommandHandler(IProjectRepository projectRepository, IProjectQueryRepository projectQueryRepository)
        {
            _projectRepository = projectRepository;
            _projectQueryRepository = projectQueryRepository;
        }

        public async Task Handle(FinishProjectCommand command, CancellationToken cancellationToken)
        {
            var toUpdate = await _projectQueryRepository.GetByIdAsync(command.Id);
            toUpdate.Finish();
            await _projectRepository.UpdateAsync(toUpdate);
            return;
        }
    }
}