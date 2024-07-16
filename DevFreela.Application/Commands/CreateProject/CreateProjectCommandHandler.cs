using DevFreela.Application.Mapper;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.CreateProject
{

    public class CreateProjectCommandHandler(IUnitOfWork unitOfWork, IProjectMapper mapper) : IRequestHandler<CreateProjectCommand, Project>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork; 
        private readonly IProjectMapper _mapper = mapper;

        public async Task<Project> Handle(CreateProjectCommand command, CancellationToken cancellationToken)
        {
            var entity = _mapper.ToEntity(command);
            entity = await _unitOfWork.ProjectRepository.AddAsync(entity);
            await _unitOfWork.CompleteAsync();
            return entity;
        }
    }

}
