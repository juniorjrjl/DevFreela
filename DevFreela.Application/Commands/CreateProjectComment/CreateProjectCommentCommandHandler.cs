using DevFreela.Application.Mapper;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.CreateProjectComment;


public class CreateProjectCommentCommandHandler : IRequestHandler<CreateProjectCommentCommand, ProjectComment>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IProjectCommentMapper _mapper;

    public CreateProjectCommentCommandHandler(IUnitOfWork unitOfWork, IProjectCommentMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ProjectComment> Handle(CreateProjectCommentCommand command, CancellationToken cancellationToken)
    {
        await _unitOfWork.ProjectQueryRepository.GetByIdAsync(command.ProjectId);
        var entity = _mapper.ToEntity(command);
        entity = await _unitOfWork.ProjectRepository.AddCommentAsync(entity);
        await _unitOfWork.CompleteAsync();
        return entity;
    }
}
