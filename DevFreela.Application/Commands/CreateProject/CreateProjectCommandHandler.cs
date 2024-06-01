using AutoMapper;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.CreateProject
{

    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Project>
    {

        private readonly IUnitOfWork _unitOfWork; 
        private readonly IMapper _mapper;

        public CreateProjectCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Project> Handle(CreateProjectCommand command, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Project>(command);
            entity = await _unitOfWork.ProjectRepository.AddAsync(entity);
            await _unitOfWork.CompleteAsync();
            return entity;
        }
    }

}
