using DevFreela.Application.Mapper;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.UpdateProject;

public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, Project>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IProjectMapper _mapper;

    public UpdateProjectCommandHandler(IUnitOfWork unitOfWork, IProjectMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper  = mapper;
    }

    public async Task<Project> Handle(UpdateProjectCommand command, CancellationToken cancellationToken)
    {
        Project toUpdate = await _unitOfWork.ProjectQueryRepository.GetByIdAsync(command.Id);
        toUpdate = _mapper.ToEntity(command, toUpdate);
        toUpdate = await _unitOfWork.ProjectRepository.UpdateAsync(toUpdate);
        await _unitOfWork.CompleteAsync();
        return toUpdate;
    }
}
