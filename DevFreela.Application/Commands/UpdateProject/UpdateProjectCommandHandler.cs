using AutoMapper;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.UpdateProject
{

    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, Project>
    {

        private readonly IProjectRepository _projectRepository;
        private readonly IProjectQueryRepository _projectQueryRepository;
        private readonly IMapper _mapper;

        public UpdateProjectCommandHandler(IProjectRepository projectRepository, IProjectQueryRepository projectQueryRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _projectQueryRepository = projectQueryRepository;
            _mapper  = mapper;
        }

        public async Task<Project> Handle(UpdateProjectCommand command, CancellationToken cancellationToken)
        {
            Project toUpdate = await _projectQueryRepository.GetByIdAsync(command.Id?? throw new ArgumentException("É necessário o id do projeto para atualiza-lo"));
            toUpdate = _mapper.Map(command, toUpdate);
            return await _projectRepository.UpdateAsync(toUpdate);
        }
    }
}