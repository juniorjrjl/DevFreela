using DevFreela.Application.Mapper;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.CreateProjectComment;


public class CreateProjectCommentCommandHandler(IUnitOfWork unitOfWork, IProjectCommentMapper mapper) : IRequestHandler<CreateProjectCommentCommand, ProjectComment>
{

    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IProjectCommentMapper _mapper = mapper;

    public async Task<ProjectComment> Handle(CreateProjectCommentCommand command, CancellationToken cancellationToken)
    {
        await _unitOfWork.ProjectQueryRepository.GetByIdAsync(command.ProjectId);
        var entity = _mapper.ToEntity(command);
        entity = await _unitOfWork.ProjectRepository.AddCommentAsync(entity);
        await _unitOfWork.CompleteAsync();
        return entity;
    }
}
