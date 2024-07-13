using DevFreela.Application.Mapper;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.CreateProject
{

    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Project>
    {

        private readonly IUnitOfWork _unitOfWork; 
        private readonly IProjectMapper _mapper;

        public CreateProjectCommandHandler(IUnitOfWork unitOfWork, IProjectMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Project> Handle(CreateProjectCommand command, CancellationToken cancellationToken)
        {
            var entity = _mapper.ToEntity(command);
            entity = await _unitOfWork.ProjectRepository.AddAsync(entity);
            await _unitOfWork.CompleteAsync();
            return entity;
        }
    }

}
