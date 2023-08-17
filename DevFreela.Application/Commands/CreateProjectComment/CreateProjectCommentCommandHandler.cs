using AutoMapper;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.CreateProjectComment
{

    public class CreateProjectCommentCommandHandler : IRequestHandler<CreateProjectCommentCommand, ProjectComment>
    {

        private readonly IProjectRepository _projectRepository; 
        private readonly IProjectQueryRepository _projectQueryRepository;
        private readonly IMapper _mapper;

        public CreateProjectCommentCommandHandler(IProjectRepository projectRepository, IProjectQueryRepository projectQueryRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _projectQueryRepository = projectQueryRepository;
            _mapper = mapper;
        }

        public async Task<ProjectComment> Handle(CreateProjectCommentCommand command, CancellationToken cancellationToken)
        {
            await _projectQueryRepository.GetByIdAsync(command.ProjectId);
            var entity = _mapper.Map<ProjectComment>(command);
            return await _projectRepository.AddCommentAsync(entity);
        }
    }

}
