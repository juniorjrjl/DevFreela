using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.StartProject
{

    public class StartProjectCommandHandler : IRequestHandler<StartProjectCommand>
    {

        private readonly IProjectRepository _projectRepository;
        private readonly IProjectQueryRepository _projectQueryRepository;

        public StartProjectCommandHandler(IProjectRepository projectRepository, IProjectQueryRepository projectQueryRepository)
        {
            _projectRepository = projectRepository;
            _projectQueryRepository = projectQueryRepository;
        }

        public async Task Handle(StartProjectCommand command, CancellationToken cancellationToken)
        {
            var toUpdate = await _projectQueryRepository.GetByIdAsync(command.Id);
            toUpdate.Start();
            await _projectRepository.UpdateAsync(toUpdate);
            return;
        }
        
    }
    
}
