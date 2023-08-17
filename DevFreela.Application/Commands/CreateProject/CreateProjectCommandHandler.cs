using AutoMapper;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.CreateProject
{

    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Project>
    {

        private readonly IProjectRepository _projectRepository; 
        private readonly IMapper _mapper;

        public CreateProjectCommandHandler(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<Project> Handle(CreateProjectCommand command, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Project>(command);
            return await _projectRepository.AddAsync(entity);
        }
    }

}
